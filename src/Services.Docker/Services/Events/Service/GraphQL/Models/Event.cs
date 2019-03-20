using System.Collections.Generic;
using GraphQL.Types;
using PingDong.DomainDriven.Service.GraphQL;
using PingDong.Newmoon.Events.Service.Queries.Models;

namespace PingDong.Newmoon.Events.Service.GraphQL.Models
{
    [GraphType]
    public class EventGraphType : ObjectGraphType<Event>
    {
        public EventGraphType()
        {
            Name = "Event";

            Field(i => i.Id);
            Field(i => i.Name);
            Field(i => i.StartTime);
            Field(i => i.EndTime);
            Field(i => i.StatusId);
            Field(i => i.PlaceId, nullable:true);
            Field<ListGraphType<AttendeeGraphType>, IEnumerable<Attendee>>("Attendees");
        }
    }
}
