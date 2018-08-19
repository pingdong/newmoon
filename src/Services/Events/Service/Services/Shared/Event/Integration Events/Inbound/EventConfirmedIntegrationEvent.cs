using PingDong.EventBus;

namespace PingDong.Newmoon.Events.Service.IntegrationEvents
{
    public class EventConfirmedIntegrationEvent : IntegrationEvent
    {
        public int EventId { get; }

        public EventConfirmedIntegrationEvent(int eventId)
        {
            EventId = eventId;
        }
            
    }
}
