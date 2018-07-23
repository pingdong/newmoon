using PingDong.DomainDriven.Service.OData;

namespace PingDong.Newmoon.Events.Service.Queries
{
    [ODataEnable("Places")]
    public class Place
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isOccupied { get; set; }
        public string addressNo { get; set; }
        public string addressStreet { get; set; }
        public string addressCity { get; set; }
        public string addressState { get; set; }
        public string addressCountry { get; set; }
        public string addressZipCode { get; set; }

    }
}
