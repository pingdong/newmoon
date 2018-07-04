using System;
using System.Collections.Generic;
using MediatR;
using PingDong.Newmoon.Events.Service.Models;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class CreateEventCommand : IRequest<bool>
    {

        public string Name { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public PlaceDTO Place { get; }

        private readonly List<AttendeeDTO> _attendees;
        public IEnumerable<AttendeeDTO> Attendees => _attendees;

        public CreateEventCommand(string name,
            DateTime start, DateTime end, List<AttendeeDTO> attendee,
            string placeName, AddressDTO address)
        {
            this.Name = name;
            this.StartTime = start;
            this.EndTime = end;

            this._attendees = attendee;
            this.Place = new PlaceDTO { Name= placeName, Address = address };
        }

    }
}
