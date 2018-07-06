using MediatR;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class RemoveAttendeeCommand : IRequest<bool>
    {
        public int EventId { get; }
        public string Identity { get; }

        public RemoveAttendeeCommand(int eventId, string identity, string firstname, string lastname)
        {
            this.EventId = eventId;

            this.Identity = identity;
        }

    }
}
