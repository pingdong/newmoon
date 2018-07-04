using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class EventStartedDomainEvent : INotification
    {
        public Event Event { get; }

        public EventStartedDomainEvent(Event evt)
        {
            Event = evt;
        }
    }
}
