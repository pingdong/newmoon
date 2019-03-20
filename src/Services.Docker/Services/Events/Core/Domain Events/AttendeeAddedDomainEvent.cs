using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class AttendeeAddedDomainEvent : INotification
    {
        public Attendee Attendee { get; }

        public AttendeeAddedDomainEvent(Attendee attendee)
        {
            Attendee = attendee;
        }
    }
}
