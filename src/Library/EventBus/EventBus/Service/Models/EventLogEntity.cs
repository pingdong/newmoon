using System;
using Newtonsoft.Json;
using PingDong.EventBus.Events;

namespace PingDong.EventBus.Services
{
    public class EventLogEntity
    {
        private EventLogEntity() { }

        public EventLogEntity(IntegrationEvent @event)
        {
            EventId = @event.Id;
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);

            State = EventStateEnum.NotPublished;
            TimesSent = 0;
        }

        public Guid EventId { get; set; }
        public string EventTypeName { get; set; }
        public DateTime CreationTime { get; set; }
        public string Content { get; set;  }

        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
    }
}
