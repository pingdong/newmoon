using MediatR;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class ApproveEventCommand : IRequest<bool>
    {
        public int EventId { get; }

        public string EventName { get; }

        public ApproveEventCommand(int eventId, string eventName)
        {
            this.EventId = eventId;
            this.EventName = eventName;
        }

    }
}
