using System;
using PingDong.Service.OData;

namespace PingDong.Newmoon.Events.Service.Queries
{
    [ODataEnable("Events")]
    public class EventSummary
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int statusId { get; set; }
    }
}
