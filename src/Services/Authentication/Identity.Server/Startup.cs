using System;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Logging;

using PingDong.Application.Logging;
using PingDong.Newmoon.IdentityServer.Authentication;
using PingDong.Newmoon.IdentityServer.Identity;
using PingDong.Newmoon.IdentityServer.Identity.Migrations;
using PingDong.Newmoon.IdentityServer.Infrastructure.Configuration;
using PingDong.Web.Exceptions;

namespace PingDong.Newmoon.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, ILogger<Startup> logger, IHostingEnvironment env)
        {
            _configuration = config;
            _logger = logger;
            _env = env;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Settings

            services.Configure<AppSettings>(_configuration);

            _logger.LogInformation(LoggingEvent.Success, "Configurations are loaded from Section: AppSettings");

            #endregion

            #region Web

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            _logger.LogInformation(LoggingEvent.Success, "Web configuration are loaded");

            #endregion

            #region DevOps

            #region Telemetry (Application Insights)

            if (_env.IsProduction())
            {
                services.AddApplicationInsightsTelemetry(_configuration);
            }

            _logger.LogInformation(LoggingEvent.Entering, "ApplicationInsights is initialized");

            #endregion

            #region HealthCheck

            services.AddHealthChecks(checks =>
            {
                checks.AddSqlCheck("Database", _configuration.GetConnectionString("DefaultDbConnection"));
            });

            _logger.LogInformation(LoggingEvent.Success, "HealthCheck Initialized");

            #endregion

            #endregion

            #region Authentication

            var authConnectionString = _configuration.GetConnectionString("DefaultDbConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(authConnectionString,
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 10,
                                    maxRetryDelay: TimeSpan.FromSeconds(30),
                                    errorNumbersToAdd: null);
                            }
                        ));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                        {
                            options.Password.RequireDigit = false;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireUppercase = false;
                        })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    // After extending IdentityUser, the below method has to be called to use default logon/register UI
                    .AddDefaultUI();

            _logger.LogInformation(LoggingEvent.Success, "Authentication Initialized");

            #endregion

            #region Identity Server 4
            
            services.AddTransient<IProfileService, ProfileService>();

            var identityBuilder = services.AddIdentityServer(options =>
                                            {
                                               
                                            })
                                          .AddAspNetIdentity<ApplicationUser>()
                                          .AddProfileService<ProfileService>();
            
            if (_env.IsDevelopment())
            {
                identityBuilder.AddDeveloperSigningCredential()
                               .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                               .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                               .AddInMemoryClients(IdentityServerConfig.GetClients());
            }
            else
            {
                var identityConnectionString = _configuration.GetConnectionString("DefaultDbConnection");
                var migrationsAssembly = GetType().GetTypeInfo().Assembly.GetName().Name;

                identityBuilder.AddConfigurationStore(options =>
                                    {
                                        options.DefaultSchema = IdentityDbContextConfig.DefaultSchema;
                                        options.ConfigureDbContext = builder => builder.UseSqlServer(identityConnectionString,
                                            sqlServerOptionsAction: sqlOptions =>
                                                {
                                                    sqlOptions.MigrationsAssembly(migrationsAssembly)
                                                              .EnableRetryOnFailure(maxRetryCount: 15,
                                                                                    maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                                errorNumbersToAdd: null);

                                                });
                                    })
                                .AddOperationalStore(options =>
                                    {
                                        options.DefaultSchema = IdentityDbContextConfig.DefaultSchema;
                                        // this enables automatic token cleanup. this is optional.
                                        options.EnableTokenCleanup = true;
                                        options.TokenCleanupInterval = 30;
                                        options.ConfigureDbContext = builder => builder.UseSqlServer(identityConnectionString,
                                            sqlServerOptionsAction: sqlOptions =>
                                                {
                                                    sqlOptions.MigrationsAssembly(migrationsAssembly)
                                                              .EnableRetryOnFailure(maxRetryCount: 15,
                                                                                    maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                                errorNumbersToAdd: null);
                                                });
                                    })
                                .AddSigningCredential(CertificateHelp.Get());
            }

            _logger.LogInformation(LoggingEvent.Success, "Initialized IdentityServer4");

            #endregion

            #region ASP.Net

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                    .AddRazorPagesOptions(o => o.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model => { foreach (var selector in model.Selectors) { var attributeRouteModel = selector.AttributeRouteModel; attributeRouteModel.Order = -1; attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length); } }))
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            _logger.LogInformation(LoggingEvent.Success, "Web Service Initialized");

            #endregion

            _logger.LogInformation(LoggingEvent.Success, "ConfigureServices");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _logger.LogInformation(LoggingEvent.Entering, "Configure Starting");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                // Make work identity server redirections in Edge and lastest versions of browers. WARN: Not valid in a production environment.
                app.Use(async (context, next) =>
                {
                    context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
                    await next();
                });

                _logger.LogInformation(LoggingEvent.Success, "In Development Environment");
            }
            else
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                    var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();

                    var seed = new IdentityConfigurationSeed();
                    seed.SeedAsync(context).Wait();
                }

                app.UseExceptionHandler("/Error");
                app.UseGlobalExceptionHandle();

                // Using https
                app.UseHsts();
                app.UseHttpsRedirection();

                loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

                _logger.LogInformation(LoggingEvent.Success, "In Production Environment");
            }
            
            app.UseStaticFiles();
            _logger.LogInformation(LoggingEvent.Success, "Handling Static Files");

            app.UseCookiePolicy();
            // Have to be use in front of IdentityServer
            // If a object is inherited from IdentityUser and DefaultUI is used.
            app.UseAuthentication();

            app.UseIdentityServer();
            _logger.LogInformation(LoggingEvent.Success, "Handling Authentication");

            app.UseMvc();
        }
    }
}
