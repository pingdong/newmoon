using PingDong.Service;

namespace PingDong.Newmoon.Events.Service.Queries
{
    public interface IEventQuery : IQuery<EventSummary>, ISingleQuery<Event>
    {
    }
}
