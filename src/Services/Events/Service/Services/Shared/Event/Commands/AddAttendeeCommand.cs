using MediatR;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class AddAttendeeCommand : IRequest<bool>
    {
        public int EventId { get; }
        public string Identity { get; }
        public string FirstName { get; }

        public string LastName { get; }

        public AddAttendeeCommand(int eventId, string identity, string firstname, string lastname)
        {
            this.EventId = eventId;

            this.Identity = identity;
            this.FirstName = firstname;
            this.LastName = lastname;
        }

    }
}
