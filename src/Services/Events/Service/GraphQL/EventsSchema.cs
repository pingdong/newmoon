using GraphQL;
using GraphQL.Types;

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
