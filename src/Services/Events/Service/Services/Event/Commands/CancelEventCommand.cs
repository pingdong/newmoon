using MediatR;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class CancelEventCommand : IRequest<bool>
    {
        public int EventId { get; }

        public string EventName { get; }

        public CancelEventCommand(int eventId, string eventName)
        {
            this.EventId = eventId;
            this.EventName = eventName;
        }

    }
}
