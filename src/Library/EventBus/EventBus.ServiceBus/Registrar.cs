using Autofac;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Application.Dependency;
using PingDong.EventBus.Abstractions;

namespace PingDong.EventBus.ServiceBus
{
    /// <summary>
    /// Register in ASP.Net Dependency Container
    /// </summary>
    public class Registrar : IDepdencyRegistrar
    {
        public DependecyType RegisterType => DependecyType.Infrastructure;

        /// <summary>
        /// Inject into dependency container
        /// </summary>
        /// <param name="services">container</param>
        /// <param name="configuration">configuration</param>
        /// <param name="loggerFactory"></param>
        public void Inject(IServiceCollection services, IConfiguration configuration, ILogger loggerFactory)
        {
            var subscriptionClientName = configuration["EventBus:ClientName"];

            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                var serviceBusConnectionString = configuration["EventBus:ConnectionString"];
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
            });

            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            {
                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                    eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
            });
        }
    }
}
