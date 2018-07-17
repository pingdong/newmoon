using System;

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
using PingDong.Newmoon.IdentityServer.Data;
using PingDong.Newmoon.Identity.Authentication;
using PingDong.Newmoon.Identity.Infrastructure.Configuration;
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
            #region Configuration

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<AppSettings>(_configuration);

            _logger.LogInformation(LoggingEvent.Success, "Configurations are loaded from Section: AppSettings");

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

            #region DataContext

            services.AddDbContext<ApplicationDbContext>(options =>

            options.UseSqlServer(_configuration.GetConnectionString("DefaultDbConnection"), 
                    sqlServerOptionsAction: sqlOptions =>
                                            {
                                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, 
                                                                                maxRetryDelay: TimeSpan.FromSeconds(30), 
                                                                            errorNumbersToAdd: null);
                                            }
                ));

            #endregion

            #region Authentication

            #region ASP.Net Authentication
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // After extending IdentityUser, the below method has to be called to use default logon/register UI
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // Alternative way to customize Asp.Net Identity 
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //});

            #endregion

            _logger.LogInformation(LoggingEvent.Success, "Authentication Initialized");

            #endregion

            #region ASP.Net

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

                _logger.LogInformation(LoggingEvent.Success, "In Development Environment");
            }
            else
            {
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
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
