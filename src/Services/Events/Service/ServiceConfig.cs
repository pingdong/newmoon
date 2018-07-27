using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore;
using PingDong.EventBus.Abstractions;
using PingDong.Newmoon.Events.Service.IntegrationEvents;

namespace PingDong.Newmoon.Events.Service
{
    public class ServiceConfig : IServiceConfigure
    {
        public void Config(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            // Register handler for all inbound integration events
            eventBus.Subscribe<EventConfirmedIntegrationEvent, IIntegrationEventHandler<EventConfirmedIntegrationEvent>>();
        }
    }
}
