using System;
using System.Collections.Generic;

namespace PingDong.Newmoon.Events.Service.Queries
{
    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string status { get; set; }
        public string place { get; set; }
        public List<Attendee> attendees { get; set; }
    }
}
