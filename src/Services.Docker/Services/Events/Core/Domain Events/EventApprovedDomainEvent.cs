using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class EventApprovedDomainEvent : INotification
    {
        public Event Event { get; }

        public EventApprovedDomainEvent(Event evt)
        {
            Event = evt;
        }
    }
}
