using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PingDong.Application.Dependency;
using PingDong.Application.Logging;
using PingDong.Linq;
using PingDong.Reflection;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using PingDong.Newmoon.Events.BackgroundTasks.Configuration;

namespace PingDong.Newmoon.Events.BackgroundTasks
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

            #region Configuration

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

                checks.AddSqlCheck("Database Connection", Configuration["SqlServer_ConnectionString"], TimeSpan.FromMinutes(minutes));
            });

            _logger.LogInformation(LoggingEvent.Success, "HealthCheck is initialized");

            #endregion

            #region Telemetry (Application Insights)

            if (_env.IsProduction())
            {
                services.AddApplicationInsightsTelemetry(Configuration);

                _logger.LogInformation(LoggingEvent.Entering, "ApplicationInsights is initialized");
            }

            #endregion

            #endregion

            #region Service Injecting (ASP.Net Core / Autofac IoC / EventBus)

            #region CQRS (MediatR)

            // Have to be initialized after injecting all necessary library by ASP.Net Core IoC

            services.AddMediatR(references);

            _logger.LogInformation(LoggingEvent.Success, "MediatR is initialized");

            #endregion

            #region Object Mapping (AutoMapper)

            // Register all mapping profiles into IoC

            services.AddAutoMapper(references);

            _logger.LogInformation(LoggingEvent.Success, "Objects Mapping are injected into IoC");

            #endregion

            #region Autofac

            _logger.LogInformation(LoggingEvent.Entering, "Autofac is starting");

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var autofaceRegistrars = references.FindSubclasses<BackgroundTaskRegistrar>();
            if (autofaceRegistrars.IsNullOrEmpty())
            {
                _logger.LogInformation(LoggingEvent.Success, "No autofac modules are found");
            }
            else
            {
                foreach (var module in autofaceRegistrars)
                {
                    module.Logger = _logger;

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

        /// <summary>
        /// Start
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Hosting Environment</param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _logger.LogInformation(LoggingEvent.Entering, "Configure Starting");

            app.Map("/liveness", lapp => lapp.Run(async ctx => ctx.Response.StatusCode = 200));

            _logger.LogInformation(LoggingEvent.Success, "Configure Started");
        }

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
                        _referencedAssemblies.Add(Assembly.LoadFrom($"{path}\\PingDong.EventBus.ServiceBus.dll"));
                        break;
                    case "rabbitmq":
                        _referencedAssemblies.Add(Assembly.LoadFrom($"{path}\\PingDong.EventBus.RabbitMQ.dll"));
                        break;
                    default:
                        _referencedAssemblies.Add(Assembly.LoadFrom($"{path}\\PingDong.EventBus.Mock.dll"));
                        break;
                }
            }

            #endregion

            return _referencedAssemblies;
        }

        private List<Assembly> _referencedAssemblies = new List<Assembly>();

        #endregion
    }

}
