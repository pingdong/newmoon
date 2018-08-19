using System;
using PingDong.DomainDriven.Service.OData;

namespace PingDong.Newmoon.Events.Service.Queries.Models
{
    [ODataEnable("Events")]
    public class EventSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StatusId { get; set; }
    }
}
