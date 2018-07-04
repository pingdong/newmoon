using MediatR;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class ConfirmEventCommand : IRequest<bool>
    {
        public int EventId { get; }

        public string EventName { get; }

        public ConfirmEventCommand(int eventId, string eventName)
        {
            this.EventId = eventId;
            this.EventName = eventName;
        }

    }
}
