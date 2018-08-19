using GraphQL.Types;
using PingDong.Newmoon.Events.Service.Queries.Models;

namespace PingDong.Newmoon.Events.Service.GraphQL.Models
{
    public class PlaceGraphType : ObjectGraphType<Place>
    {
        public PlaceGraphType()
        {
            Name = "Place";

            Field(i => i.Id);
            Field(i => i.Name);
            Field(i => i.IsOccupied);
            Field(i => i.AddressNo);
            Field(i => i.AddressStreet);
            Field(i => i.AddressCity);
            Field(i => i.AddressState);
            Field(i => i.AddressCountry);
            Field(i => i.AddressZipCode);
        }
    }
}
