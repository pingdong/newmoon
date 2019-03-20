using System;
using MediatR;
using PingDong.Newmoon.Events.Service.Commands.Models;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class UpdateEventCommand : IRequest<bool>
    {
        public int Id { get; }
        public string Name { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public PlaceDTO Place { get; }

        public UpdateEventCommand(int id, string name,
            DateTime start, DateTime end,
            string placeName, AddressDTO address)
        {
            this.Id = id;
            this.Name = name;
            this.StartTime = start;
            this.EndTime = end;

            this.Place = new PlaceDTO { Name = placeName, Address = address };
        }

    }
}
