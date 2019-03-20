using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class AttendeeRemovedDomainEvent : INotification
    {
        public Attendee Attendee { get; }

        public AttendeeRemovedDomainEvent(Attendee attendee)
        {
            Attendee = attendee;
        }
    }
}
