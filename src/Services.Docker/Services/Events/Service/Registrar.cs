﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Application.Dependency;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Service.Commands.Idempotent;
using PingDong.Newmoon.Events.Service.IntegrationEvents;

namespace PingDong.Newmoon.Events.Service
{
    /// <summary>
    /// Register in ASP.Net Dependency Container
    /// </summary>
    public class Registrar : IDepdencyRegistrar
    {
        public DependecyType RegisterType => DependecyType.Service;

        /// <summary>
        /// Inject into dependency container
        /// </summary>
        /// <param name="services">container</param>
        /// <param name="configuration">configuration</param>
        /// <param name="loggerFactory"></param>
        public void Inject(IServiceCollection services, IConfiguration configuration, ILogger loggerFactory)
        {
            // Request Manager
            services.AddTransient<IRequestManager, RequestManager>();

            // Event Bus
            if (configuration.GetValue("EventBus:Enabled", false))
            {
                services.AddTransient<IIntegrationEventService, IntegrationEventService>();
            }
            else
            {
                services.AddTransient<IIntegrationEventService, EmptyIntegrationEventService>();
            }
        }
    }
}