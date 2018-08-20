using GraphQL.Types;
using PingDong.Newmoon.Events.Service.Queries.Models;

namespace PingDong.Newmoon.Events.Service.GraphQL.Models
{
    public class EventSummaryGraphType : ObjectGraphType<EventSummary>
    {
        public EventSummaryGraphType()
        {
            Name = "EventSummary";

            Field(i => i.Id);
            Field(i => i.Name);
            Field(i => i.StartTime);
            Field(i => i.EndTime);
            Field(i => i.StatusId);
        }
    }
}
