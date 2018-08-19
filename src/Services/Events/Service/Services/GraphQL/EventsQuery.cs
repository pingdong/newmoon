using GraphQL.Types;
using PingDong.Newmoon.Events.Service.GraphQL.Models;
using PingDong.Newmoon.Events.Service.Queries.Rest;

namespace PingDong.Newmoon.Events.Service.GraphQL.Queries
{
    public class EventsQuery : ObjectGraphType
    {
        public EventsQuery(IPlaceQuery placeQuery, IEventQuery eventQuery)
        {
            Name = "Query";

            // { "query": "{ places { id name } }" }
            FieldAsync<ListGraphType<PlaceGraphType>>(
                name: "places",
                resolve: async context => await placeQuery.GetAllAsync()
                );

            // { "query": "{ events { id name } }" }
            FieldAsync<ListGraphType<EventSummaryGraphType>>(
                name: "events",
                resolve: async context => await eventQuery.GetAllAsync()
            );

        }
    }
}
