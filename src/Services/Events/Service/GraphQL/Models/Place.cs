using GraphQL.Types;
using PingDong.DomainDriven.Service.GraphQL;
using PingDong.Newmoon.Events.Service.Queries.Models;

namespace PingDong.Newmoon.Events.Service.GraphQL.Models
{
    [GraphType]
    public class PlaceGraphType : ObjectGraphType<Place>
    {
        public PlaceGraphType()
        {
            Name = "Place";

            Field(i => i.Id);
            Field(i => i.Name);
            Field(i => i.IsOccupied);
            Field(i => i.AddressNo, nullable:true);
            Field(i => i.AddressStreet, nullable: true);
            Field(i => i.AddressCity, nullable: true);
            Field(i => i.AddressState, nullable: true);
            Field(i => i.AddressCountry, nullable: true);
            Field(i => i.AddressZipCode, nullable: true);
        }
    }
}
