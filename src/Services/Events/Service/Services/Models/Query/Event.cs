using System;
using System.Collections.Generic;

namespace PingDong.Newmoon.Events.Service.Queries.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StatusId { get; set; }
        public int PlaceId { get; set; }
        public List<Attendee> Attendees { get; set; }
    }
}
