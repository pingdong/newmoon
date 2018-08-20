using MediatR;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class StartEventCommand : IRequest<bool>
    {
        public int EventId { get; }

        public string EventName { get; }

        public StartEventCommand(int eventId, string eventName)
        {
            this.EventId = eventId;
            this.EventName = eventName;
        }

    }
}
