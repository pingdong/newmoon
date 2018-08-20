using MediatR;
using PingDong.Newmoon.Events.Service.Commands.Models;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class UpdatePlaceCommand : IRequest<bool>
    {
        public int Id { get; }

        public string Name { get; }

        public AddressDTO Address { get; }

        public UpdatePlaceCommand(int id, string name, AddressDTO address)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
        }

    }
}
