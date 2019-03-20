using PingDong.DomainDriven.Service.OData;

namespace PingDong.Newmoon.Events.Service.Queries.Models
{
    [ODataEnable("Places")]
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOccupied { get; set; }
        public string AddressNo { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipCode { get; set; }

    }
}
