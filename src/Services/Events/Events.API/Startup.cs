using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;

using PingDong.Application.Dependency;
using PingDong.Application.Logging;
using PingDong.Linq;
using PingDong.Newmoon.Events.Configuration;
using PingDong.Reflection;
using PingDong.AspNetCore.Http;
using PingDong.AspNetCore.Mvc.Filters;
using PingDong.DomainDriven.Service.OData;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using GraphQL;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PingDong.EventBus;
using PingDong.Newmoon.Events.Filters;
using PingDong.Newmoon.Events.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using StackExchange.Redis;

namespace PingDong.Newmoon.Events
{
    /// <summary>
    /// Bootstrap code
    /// </summary>
    public class Startup
    {
        #region Variable Declare

        private readonly IHostingEnvironment _env;
        private readonly ILogger _logger;

        #endregion

        #region ctor

        /// <inheritdoc />
        public Startup(IConfiguration config, ILogger<Startup> logger, IHostingEnvironment env)
        {
            Configuration = config;
            _logger = logger;
            _env = env;
        }

        #endregion

        #region Configuration

        /// <summary>
        /// Configuration
        /// </summary>
        protected IConfiguration Configuration { get; }
        /// <summary>
        /// AppSettings
        /// </summary>
        protected AppSettings AppSettings { get; private set; }

        #endregion

        /// <summary>
        /// Create and register services
        /// </summary>
        /// <param name="services">Services Collection</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation(LoggingEvent.Entering, "ConfigureServices Starting");

            #region Loading assemblies

            // Get all target assemblies for IoC registration
            // Avoiding searching in the 3rd-party libraries, like Nuget packages

            var references = GetSearchingTargets();

            _logger.LogInformation(LoggingEvent.Success, $"{ references.Count } assemblies are referenced");
            references.ForEach(r => _logger.LogInformation($"{r.FullName} is loaded"));
            
            #endregion

            #region Configuration (ASP.Net Core)

            // Extract AppSettings and register into IoC

            AppSettings = Configuration.GetSection("App").Get<AppSettings>();
            services.AddSingleton(AppSettings);

            _logger.LogInformation(LoggingEvent.Success, "Configurations are loaded from Section: App");

            #endregion

            #region DevOps

            #region Health (HealthChecks)

            services.AddHealthChecks(checks =>
            {
                int minutes = 5;
                if (AppSettings.HealthCheckInterval != 0)
                {
                    minutes = AppSettings.HealthCheckInterval;
                }

                checks.AddSqlCheck("Database Connection", Configuration["SqlServer_ConnectionString"], TimeSpan.FromMinutes(minutes))
                      .AddUrlCheck(Configuration["IdentityServiceUri"], TimeSpan.FromMinutes(minutes));

                // For isolated web service only, doesn't depend on any db or service
                // checks.AddValueTaskCheck("HTTP Endpoint", () => new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
            });

            _logger.LogInformation(LoggingEvent.Success, "HealthCheck is initialized");

            #endregion

            #region Swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(AppSettings.ApiVersion, new Info
                    {
                        Title = AppSettings.Title,
                        Version = AppSettings.Version,
                        Description = $"{AppSettings.Title} v{AppSettings.Version}"
                    });
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                    {
                        Description = "OAuth2 Authentication using Identity.Server",
                        AuthorizationUrl = $"{Configuration["IdentityServiceUri"]}/connect/authorize",
                        TokenUrl = $"{Configuration["IdentityServiceUri"]}/connect/token",
                        Flow = "implicit",
                        Type = "oauth2",
                        Scopes = new Dictionary<string, string>
                            {
                                { "events.api", "Api Scope"},
                                { "openid", "OpenId" },
                                { "email", "Email" },
                                { "profile", "Profile" },
                            }
                    });
                // Send authorization token in header
                options.DocumentFilter<SecurityRequirementsDocumentFilter>();

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, $"{Assembly.GetEntryAssembly().GetName().Name}.xml");
                _logger.LogInformation(LoggingEvent.Success, $"{xmlPath} is loading");

                if (!bool.TrueString.Equals(Configuration["isTest"], StringComparison.InvariantCultureIgnoreCase))
                {
                    options.IncludeXmlComments(xmlPath);
                }
                options.DescribeAllEnumsAsStrings();
            });

            _logger.LogInformation(LoggingEvent.Success, "Swagger is initialized");

            #endregion
            
            #region Telemetry (Application Insights)

            if (_env.IsProduction())
            {
                services.AddApplicationInsightsTelemetry(Configuration);

                _logger.LogInformation(LoggingEvent.Entering, "ApplicationInsights is initialized");
            }

            #endregion

            #endregion

            #region Caching (In-memory / Distributed)

            // InMemory
            services.AddMemoryCache();

            // Distributed Cache (Microsoft Redis implementation)
            var redisServer = Configuration["DistributedCache:Server"];
            var redisInstance = Configuration["DistributedCache:Instance"];
            services.AddDistributedRedisCache(option =>
                {
                    option.Configuration = redisServer;
                    option.InstanceName = redisInstance;
                });

            // Redis (StackExchange)
            // Making sure the service won't start until redis is ready.
            services.AddSingleton(_ =>
                {
                    var redisConnectionString = Configuration["Redis:ConnectionString"];
                    var configuration = ConfigurationOptions.Parse(redisConnectionString, true);

                    configuration.ResolveDns = true;

                    return ConnectionMultiplexer.Connect(configuration);
                });

            #endregion

            #region Identity

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                        {
                            options.Authority = Configuration["IdentityServiceUri"];
                            options.ApiName = "Events Api";
                            options.ApiSecret = "events_api-client";
                            options.LegacyAudienceValidation = true; // It is required for 401 error ValidAudiences
                            options.RequireHttpsMetadata = _env.IsProduction();
                        });

            _logger.LogInformation(LoggingEvent.Success, "Identity Validation is initialized");

            #endregion

            #region ASP.Net

            // OData
            services.AddOData();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            // What's different between AddMvc and AddMvcCore
            // https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/
            services.AddMvcCore(options =>
                        {
                            // Checking authentication
                            var policy = new AuthorizationPolicyBuilder()
                                                    // User must be authenticated
                                                    .RequireAuthenticatedUser()
                                                    // User must have scope
                                                    .RequireClaim(JwtClaimTypes.Scope, "events.api")
                                                    .Build();
                            options.Filters.Add(new AuthorizeFilter(policy));

                            // Checking ModelState
                            options.Filters.Add(new ModelStateValidationFilter(_logger));

                            // Workaround: https://github.com/OData/WebApi/issues/1177
                            foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                            {
                                outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                            }
                            foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                            {
                                inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                            }
                        })
                    // Using FluentValidation to verify incoming requests
                    .AddFluentValidation(fvc => { references.ForEach(v => fvc.RegisterValidatorsFromAssembly(v)); })
                    // For swagger
                    .AddApiExplorer()
                    // Security
                    .AddCors(options =>
                        {
                            // this defines a CORS policy called "default"
                            options.AddPolicy("default", policy =>
                            {
                                policy.WithOrigins(Configuration["EventsServiceUri"])
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials();
                            });
                        })
                    .AddAuthorization(options =>
                        {
                            // Define predefined policy, then it can be used in controller
                            //    [Authorize(Policy = "RequireAdministratorRole")]
                            //    public IActionResult Shutdown()
                            options.AddPolicy("RequireAdministratorRole", policy =>
                            {
                                policy.RequireScope("admin");
                            });
                        })
                    // Json
                    .AddJsonFormatters()
                    .AddJsonOptions(
                            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        )
                    // http://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#controllers-as-services
                    // https://www.strathweb.com/2016/03/the-subtle-perils-of-controller-dependency-injection-in-asp-net-core-mvc/
                    //.AddControllersAsServices()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    ;
            // Config A
            services.Configure<ApiBehaviorOptions>(options =>
                {
                    //options.SuppressConsumesConstraintForFormFileParameters = true;
                    //options.SuppressInferBindingSourcesForParameters = true;
                    //options.SuppressModelStateInvalidFilter = true;
                });

            _logger.LogInformation(LoggingEvent.Success, "MVC is initialized");

            #endregion

            #region Service Injecting (ASP.Net Core / Autofac IoC)

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            #region GraphQL

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();

            #endregion

            #region CQRS (MediatR)

            // Have to be initialized after injecting all necessary library by ASP.Net Core IoC

            services.AddMediatR(references.ToArray());

            _logger.LogInformation(LoggingEvent.Success, "MediatR is initialized");

            #endregion

            #region Object Mapping (AutoMapper)

                // Register all mapping profiles into IoC

                services.AddAutoMapper(references);

            _logger.LogInformation(LoggingEvent.Success, "Objects Mapping are injected into IoC");

            #endregion

            #region Auto discovery and register

            var dependecies = references.FindInterfaces<IDepdencyRegistrar>();
            if (!dependecies.IsNullOrEmpty())
            {
                var instances = dependecies.OrderBy(d => d.RegisterType);
                foreach (var instance in instances)
                {
                    instance.Inject(services, Configuration, _logger);
                    _logger.LogDebug(LoggingEvent.Success, $"{instance.GetType().FullName} is injected");
                }
            }

            _logger.LogInformation(LoggingEvent.Success, "Services are injected");

            #endregion

            #region Autofac

            _logger.LogInformation(LoggingEvent.Entering, "Autofac is starting");

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var autofaceRegistrars = references.FindSubclasses<AutofacDependencyRegistrar>();
            if (autofaceRegistrars.IsNullOrEmpty())
            {
                _logger.LogInformation(LoggingEvent.Success, "No autofac modules are found");
            }
            else
            {
                var modules = autofaceRegistrars.OrderBy(d => d.RegisterType);
                foreach (var module in modules)
                {
                    module.Logger = _logger;
                    module.Configuration = Configuration;

                    builder.RegisterModule(module);

                    _logger.LogInformation(LoggingEvent.Success, $"{module.GetType().FullName} is injected");
                }
            }

            // Have to be run after registering all services
            ApplicationContainer = builder.Build();

            _logger.LogInformation(LoggingEvent.Success, "Autofac is initialized");

            #endregion

            #endregion

            _logger.LogInformation(LoggingEvent.Success, "Calling ConfigureServices() returned success");

            return new AutofacServiceProvider(ApplicationContainer);
        }

        /// <summary>
        /// Autofac Container
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        #region Autofac with ConfigureContainer

        // TestServer, in Functional/Integration Test, doesn't support this way 

        ///// <summary>
        ///// ConfigureContainer is where you can register things directly
        ///// with Autofac. This runs after ConfigureServices so the things
        ///// here will override registrations made in ConfigureServices.
        ///// Don't build the container; that gets done for you.
        ///// 
        ///// http://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html
        /////
        ///// DO NOT USE THIS WAY IN MULTIPLE TENANT SCENARIO.
        ///// </summary>
        ///// <param name="builder">Container Builder</param>
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    _logger.LogInformation(LoggingEvent.Entering, "Autofac is starting");

        //    var references = GetSearchingTargets();

        //    var autofaceRegistrars = references.FindSubclasses<AutofacDependencyRegistrar>();
        //    if (autofaceRegistrars.IsNullOrEmpty())
        //    {
        //        _logger.LogInformation(LoggingEvent.Success, "No autofac modules are found");
        //        return;
        //    }

        //    var modules = autofaceRegistrars.OrderBy(d => d.RegisterType);
        //    foreach (var module in modules)
        //    {
        //        module.Logger = _logger;
        //        module.Configuration = _configuration;

        //        builder.RegisterModule(module);

        //        _logger.LogInformation(LoggingEvent.Success, $"{module.GetType().FullName} is injected");
        //    }

        //    _logger.LogInformation(LoggingEvent.Success, "Autofac is initialized");
        //}

        #endregion

        /// <summary>
        /// Start
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Hosting Environment</param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _logger.LogInformation(LoggingEvent.Entering, "Configure Starting");

            // Initialize Events Bus
            if (Configuration.GetValue("EventBus:Enabled", false))
            {
                app.SubscribeIntegrationEvents(GetSearchingTargets());
                _logger.LogInformation(LoggingEvent.Success, "EventBus registrars are executed");
            }

            app.Map("/liveness", lapp => lapp.Run(async ctx => ctx.Response.StatusCode = 200));

            if (env.IsDevelopment())
            {
                _logger.LogInformation(LoggingEvent.Success, "Running in Development environment");

                // Error message
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(); 
            }
            else
            {
                _logger.LogInformation(LoggingEvent.Success, "Running in Production environment");

                app.UseGlobalExceptionHandle();

                // Using https
                app.UseHsts();
                app.UseHttpsRedirection();

                loggerFactory.AddAzureWebAppDiagnostics();
                loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);
            }

            // Swagger support
            app.UseSwagger()
               .UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint($"{Configuration["EventsServiceUri"]}/swagger/{AppSettings.ApiVersion}/swagger.json", $"{AppSettings.Title} {AppSettings.ApiVersion}");
                        options.DefaultModelsExpandDepth(-1); // Hide Models section
                        // Authentication
                        options.OAuthAppName("Events Service");
                        options.OAuthClientId("swagger");
                        options.OAuth2RedirectUrl($"{Configuration["EventsServiceUri"]}/swagger/oauth2-redirect.html");
                    });
            _logger.LogInformation(LoggingEvent.Success, "Swagger is running");

            // Security
            app.UseCookiePolicy();
            app.UseCors("default");
            UseAuth(app);
            _logger.LogInformation(LoggingEvent.Success, "Handling Authentication");

            // GraphQL
            app.UseGraphQL();

            // MVC
            app.UseMvc(routes => 
                {
                    // Workaround: https://github.com/OData/WebApi/issues/1175
                    routes.EnableDependencyInjection();

                    var baseUri = $"api/{AppSettings.ApiVersion}";

                    // OData
                    var model = GetEdmModel(GetSearchingTargets());
                    routes.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                    routes.MapODataServiceRoute("odata", "api/v1/odata", model);

                    // Default
                    routes.MapRoute(
                        name: "default",
                        template: baseUri + "/{controller=Ping}");
                });

            _logger.LogInformation(LoggingEvent.Success, "Web Access Handling");
        }

        #region Test Support

        /// <summary>
        /// Config Authentication
        /// </summary>
        /// <param name="app"></param>
        protected virtual void UseAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }

        #endregion

        #region Private Methods

        private List<Assembly> GetSearchingTargets()
        {
            if (_referencedAssemblies.Any())
                return _referencedAssemblies;
            
            _referencedAssemblies = this.GetType().Assembly.GetReferenceAssemblies(prefix: "PingDong.Newmoon.Events");

            #region Event Bus

            // Dynamic inject all EventBus services

            if (Configuration.GetValue("EventBus:Enabled", false))
            {
                var path = this.GetType().Assembly.GetDirectoryName();

                _referencedAssemblies.Add(Assembly.LoadFrom($"{path}\\PingDong.EventBus.dll"));

                switch (Configuration["EventBus:Provider"].ToLower())
                {
                    case "azureservicebus":
                        _referencedAssemblies.Add(Assembly.LoadFrom($"{path}\\PingDong.EventBus.AzureServiceBus.dll"));
                        break;
                    case "rabbitmq":
                        _referencedAssemblies.Add(Assembly.LoadFrom($"{path}\\PingDong.EventBus.RabbitMQ.dll"));
                        break;
                }
            }

            #endregion

            return _referencedAssemblies;
        }

        private List<Assembly> _referencedAssemblies = new List<Assembly>();

        #endregion

        #region OData

        private static IEdmModel GetEdmModel(IEnumerable<Assembly> references)
        {
            var builder = new ODataConventionModelBuilder();

            var types = references.FindAttribute<ODataEnableAttribute>(typeof(ODataEnableAttribute), attribute => attribute.Enabled);
            foreach (var type in types)
            {
                var attribute = (ODataEnableAttribute)Attribute.GetCustomAttribute(type, typeof(ODataEnableAttribute));
                var entityType = builder.AddEntityType(type);
                builder.AddEntitySet(attribute.Name, entityType);
            }

            // In most cases, could use the below code to register entity
            //builder.EntitySet<EventSummary>("Events");

            return builder.GetEdmModel();
        }

        #endregion
    }

}
