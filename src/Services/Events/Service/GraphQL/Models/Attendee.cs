using GraphQL.Types;
using PingDong.Newmoon.Events.Service.Queries.Models;

namespace PingDong.Newmoon.Events.Service.GraphQL.Models
{
    public class AttendeeGraphType : ObjectGraphType<Attendee>
    {
        public AttendeeGraphType()
        {
            Name = "Attendee";

            Field(i => i.Id);
            Field(i => i.Firstname);
            Field(i => i.Lastname);
        }
    }
}
