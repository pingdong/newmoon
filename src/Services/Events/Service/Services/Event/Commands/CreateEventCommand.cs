using System;
using System.Collections.Generic;
using MediatR;
using PingDong.Newmoon.Events.Service.Models;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class CreateEventCommand : IRequest<bool>
    {

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        
        public PlaceDTO Place { get; set; }
        
        public IEnumerable<AttendeeDTO> Attendees { get; set; }

    }
}
