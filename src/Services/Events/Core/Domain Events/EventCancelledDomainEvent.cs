using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class EventCancelledDomainEvent : INotification
    {
        public Event Event { get; }

        public EventCancelledDomainEvent(Event evt)
        {
            Event = evt;
        }
    }
}
