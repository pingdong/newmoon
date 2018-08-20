using MediatR;
using PingDong.Newmoon.Events.Service.Commands.Models;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class CreatePlaceCommand : IRequest<bool>
    {
        public string Name { get; }

        public AddressDTO Address { get; }

        public CreatePlaceCommand(string name, AddressDTO address)
        {
            this.Name = name;
            this.Address = address;
        }
    }
}
