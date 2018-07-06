using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class EventConfirmedDomainEvent : INotification
    {
        public Event Event { get; }

        public EventConfirmedDomainEvent(Event evt)
        {
            Event = evt;
        }
    }
}
