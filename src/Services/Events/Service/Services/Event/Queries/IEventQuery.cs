using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Service.Queries.Models;

namespace PingDong.Newmoon.Events.Service.Queries
{
    public interface IEventQuery : IQuery<EventSummary>, ISingleQuery<Event>
    {
    }
}
