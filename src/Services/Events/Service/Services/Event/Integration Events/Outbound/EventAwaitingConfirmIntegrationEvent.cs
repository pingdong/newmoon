using System;
using PingDong.EventBus.Events;

namespace PingDong.Newmoon.Events.Service.IntegrationEvents
{
    public class EventAwaitingConfirmIntegrationEvent : IntegrationEvent
    {
        public int EventId { get; }
        public string EventName { get; }
        public string Place { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

        public EventAwaitingConfirmIntegrationEvent(int eventId, string eventName, string place, DateTime start, DateTime end)
        {
            EventId = eventId;
            EventName = eventName;
            Place = place;
            Start = start;
            End = end;
        }
            
    }
}
