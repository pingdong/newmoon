using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class EventUpdatedDomainEvent : INotification
    {
        public Event Event { get; }

        public EventUpdatedDomainEvent(Event evt)
        {
            Event = evt;
        }
    }
}
