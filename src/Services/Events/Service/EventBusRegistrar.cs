using PingDong.EventBus;
using PingDong.EventBus.Abstractions;
using PingDong.Newmoon.Events.Service.IntegrationEvents;

namespace PingDong.Newmoon.Events.Service
{
    public class EventBusRegistrar : IEventBusSubscription
    {
        public void Subscribe(IEventBus eventBus)
        {
            // Register handler for all inbound integration events
            eventBus.Subscribe<EventConfirmedIntegrationEvent, IIntegrationEventHandler<EventConfirmedIntegrationEvent>>();
        }
    }
}
