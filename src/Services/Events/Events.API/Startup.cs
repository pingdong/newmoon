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
using PingDong.Web.Exceptions;
using PingDong.Web.Validation;
using PingDong.Service.OData;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Swashbuckle.AspNetCore.Swagger;

namespace PingDong.Newmoon.Events
{
    /// <summary>
    /// Bootstrap code
    /// </summary>
    public class Startup
    {
        #region Variable Declare

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private AppSettings _appSettings;

        #endregion

        #region ctor

        /// <inheritdoc />
        public Startup(IConfiguration config, ILogger<Startup> logger, IHostingEnvironment env)
        {
            _configuration = config;
            _logger = logger;
            _env = env;
        }

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

            _appSettings = _configuration.GetSection("App").Get<AppSettings>();
            services.AddSingleton(_appSettings);

            _logger.LogInformation(LoggingEvent.Success, "Configurations are loaded from Section: App");

            #endregion
            
            #region Health (HealthChecks)

            services.AddHealthChecks(checks =>
            {
                checks.AddSqlCheck("Database Connection", _configuration["ConnectionStrings:DefaultDbConnection"]);

                // For isolated web service only, doesn't depend on any db or service
                // checks.AddValueTaskCheck("HTTP Endpoint", () => new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
            });

            _logger.LogInformation(LoggingEvent.Success, "HealthCheck is initialized");

            #endregion

            if (_env.IsDevelopment())
            {
                _logger.LogInformation(LoggingEvent.Entering, "Running in Development mode");

                #region DevOps (Swagger)

                services.AddSwaggerGen(option =>
                    {
                        option.SwaggerDoc(_appSettings.ApiVersion, new Info
                            {
                                Title = _appSettings.Title,
                                Version = _appSettings.Version,
                                Description = $"{_appSettings.Title} v{_appSettings.Version}"
                            });

                        var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                        var xmlPath = Path.Combine(basePath, $"{Assembly.GetEntryAssembly().GetName().Name}.xml");
                        _logger.LogInformation(LoggingEvent.Success, $"{xmlPath} is loading");

                        option.IncludeXmlComments(xmlPath);
                        option.DescribeAllEnumsAsStrings();
                    });


                _logger.LogInformation(LoggingEvent.Success, "Swagger is initialized");

                #endregion
            }
            else if (_env.IsStaging())
            {
                _logger.LogInformation(LoggingEvent.Entering, "Running in Stagging mode");

                // Skipping Swagger for Functional/Integration testing
            }
            else
            {
                _logger.LogInformation(LoggingEvent.Entering, "Running in Production mode");

                #region Telemetry (Application Insights)

                services.AddApplicationInsightsTelemetry(_configuration);

                _logger.LogInformation(LoggingEvent.Entering, "ApplicationInsights is initialized");

                #endregion
            }

            #region Service Injecting (ASP.Net Core IoC)

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            
            // Auto discovery and register
            var dependecies = references.FindInterfaces<IDepdencyRegistrar>();
            if (!dependecies.IsNullOrEmpty())
            {
                var instances = dependecies.OrderBy(d => d.RegisterType);
                foreach (var instance in instances)
                {
                    instance.Inject(services, _configuration, _logger);
                    _logger.LogDebug(LoggingEvent.Success, $"{instance.GetType().FullName} is injected");
                }
            }

            _logger.LogInformation(LoggingEvent.Success, "Services are injected");

            #region Object Mapping (AutoMapper)

            // Register all mapping profiles into IoC

            services.AddAutoMapper(references);

            _logger.LogInformation(LoggingEvent.Success, "Objects Mapping are injected into IoC");

            #endregion

            #endregion

            #region CQRS (MediatR)

            // Have to be initialized after injecting all necessary library by ASP.Net Core IoC

            services.AddMediatR(references);

            _logger.LogInformation(LoggingEvent.Success, "MediatR is initialized");

            #endregion

            #region Security

            #region Security (ASP.Net Core)

            services.AddCors(options =>
                {
                    // this defines a CORS policy called "default"
                    options.AddPolicy("default", policy =>
                        {
                            policy.WithOrigins(_appSettings.BaseUri)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials();
                        });
                });

            _logger.LogInformation(LoggingEvent.Success, "Securities are initialized");

            #endregion

            #endregion

            #region ASP.Net

            services.AddOData();

            services.AddMvc(config =>
                        {
                            // Checking ModelState
                            config.Filters.Add(new ModelStateValidationFilter(_logger));
                        })
                    // Using FluentValidation to verify incoming requests
                    .AddFluentValidation(fvc => { references.ForEach(v => fvc.RegisterValidatorsFromAssembly(v)); })
                    // Customize Json
                    .AddJsonOptions(
                            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        )
                    // http://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#controllers-as-services
                    // https://www.strathweb.com/2016/03/the-subtle-perils-of-controller-dependency-injection-in-asp-net-core-mvc/
                    //.AddControllersAsServices()
                    ;

            // Workaround: https://github.com/OData/WebApi/issues/1177
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            _logger.LogInformation(LoggingEvent.Success, "MVC is initialized");

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
                    module.Configuration = _configuration;

                    builder.RegisterModule(module);

                    _logger.LogInformation(LoggingEvent.Success, $"{module.GetType().FullName} is injected");
                }
            }

            ApplicationContainer = builder.Build();

            _logger.LogInformation(LoggingEvent.Success, "Autofac is initialized");

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

        /// <summary>
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you.
        /// 
        /// http://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html
        ///
        /// DO NOT USE THIS WAY IN MULTIPLE TENANT SCENARIO.
        /// </summary>
        /// <param name="builder">Container Builder</param>
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
            
            if (env.IsDevelopment())
            {
                _logger.LogInformation(LoggingEvent.Success, "Running in Development environment");

                // Error message
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(); 

                // Swagger support
                app.UseSwagger()
                   .UseSwaggerUI(option =>
                   {
                       option.SwaggerEndpoint($"{_appSettings.BaseUri}/swagger/{_appSettings.ApiVersion}/swagger.json", $"{_appSettings.Title} {_appSettings.ApiVersion}");
                       option.DefaultModelsExpandDepth(-1); // Hide Models section
                   });

                _logger.LogInformation(LoggingEvent.Success, "Swagger is running");
            }
            else if (env.IsStaging())
            {
                _logger.LogInformation(LoggingEvent.Success, "Running in Stagging environment");

                // Error message
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                _logger.LogInformation(LoggingEvent.Success, "Running in Production environment");

                app.UseGlobalExceptionHandle();

                loggerFactory.AddAzureWebAppDiagnostics();
                loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);
            }

            app.UseCors("default");

            _logger.LogInformation(LoggingEvent.Success, "Handling Authentication");

            // MVC
            app.UseMvc(routes =>
                { 
                    // Workaround: https://github.com/OData/WebApi/issues/1175
                    routes.EnableDependencyInjection();

                    var baseUri = $"api/{_appSettings.ApiVersion}";
                    var odataUri = $"{baseUri}/odata";
                    routes.MapODataServiceRoute(odataUri, odataUri, GetEdmModel(GetSearchingTargets()));

                    routes.MapRoute(
                        name: "default",
                        template: baseUri + "/{controller=Ping}");
                });

            _logger.LogInformation(LoggingEvent.Success, "Web Access Handling");
        }

        #region Private Methods

        private List<Assembly> GetSearchingTargets()
        {
            if (_referencedAssemblies.Any())
                return _referencedAssemblies;

            _referencedAssemblies = this.GetType().Assembly.GetReferenceAssemblies(prefix: "PingDong.Newmoon.Events");

            return _referencedAssemblies;
        }

        private List<Assembly> _referencedAssemblies = new List<Assembly>();

        #endregion

        #region OData

        private static IEdmModel GetEdmModel(IEnumerable<Assembly> references)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

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
