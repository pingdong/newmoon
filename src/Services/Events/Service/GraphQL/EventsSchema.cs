using GraphQL;
using GraphQL.Types;
using PingDong.Newmoon.Events.Service.GraphQL.Queries;

namespace PingDong.Newmoon.Events.Service.GraphQL
{
    public class EventsSchema : Schema
    {
        public EventsSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<EventsQuery>();
        }
    }
}
