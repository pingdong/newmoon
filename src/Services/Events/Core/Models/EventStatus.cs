using System;
using System.Collections.Generic;
using System.Linq;
using PingDong.DomainDriven.Core;
using PingDong.Newmoon.Events.Core.Exceptions;

namespace PingDong.Newmoon.Events.Core
{
    public class EventStatus : Enumeration
    {
        public static EventStatus Created = new EventStatus(1, nameof(Created).ToLowerInvariant());
        public static EventStatus Confirmed = new EventStatus(2, nameof(Confirmed).ToLowerInvariant());
        public static EventStatus Ongoing = new EventStatus(3, nameof(Ongoing).ToLowerInvariant());
        public static EventStatus Ended = new EventStatus(4, nameof(Ended).ToLowerInvariant());
        public static EventStatus Cancelled = new EventStatus(5, nameof(Cancelled).ToLowerInvariant());
        
        public EventStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<EventStatus> List() => new[] { Created, Confirmed, Ongoing, Ended, Cancelled };

        public static EventStatus From(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new EventDomainException($"Possible values for EventStatus: { string.Join(",", List().Select(s => s.Name)) }");

            return state;
        }

        public static EventStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new EventDomainException($"Possible values for EventStatus: { string.Join(",", List().Select(s => s.Id)) }");
            
            return state;
        }
    }
}
