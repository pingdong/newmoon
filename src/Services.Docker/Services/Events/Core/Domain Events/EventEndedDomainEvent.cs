using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class EventEndedDomainEvent : INotification
    {
        public Event Event { get; }

        public EventEndedDomainEvent(Event evt)
        {
            Event = evt;
        }
    }
}
