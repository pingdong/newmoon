using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Application.Dependency;
using PingDong.EventBus.Abstractions;
using RabbitMQ.Client;

namespace PingDong.EventBus.RabbitMQ
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

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory
                {
                    HostName = configuration["EventBus:ConnectionString"]
                };

                if (!string.IsNullOrEmpty(configuration["EventBus:RabbitMQ:UserName"]))
                {
                    factory.UserName = configuration["EventBus:RabbitMQ:UserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBus:RabbitMQ:Password"]))
                {
                    factory.Password = configuration["EventBus:RabbitMQ:Password"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBus:RabbitMQ:RetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBus:RabbitMQ:RetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBus:RabbitMQ:RetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBus:RabbitMQ:RetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });
        }
    }
}
