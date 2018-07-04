using MediatR;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class EndEventCommand : IRequest<bool>
    {
        public int EventId { get; }

        public string EventName { get; }

        public EndEventCommand(int eventId, string eventName)
        {
            this.EventId = eventId;
            this.EventName = eventName;
        }

    }
}
