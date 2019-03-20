using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, bool>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;

        public CreatePlaceCommandHandler(IPlaceRepository placeRepository, IMapper mapper)
        {
            _placeRepository = placeRepository ?? throw new ArgumentNullException(nameof(placeRepository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePlaceCommand message, CancellationToken cancellationToken)
        {
            // TODO: Value Object
            //var address = _mapper.Map<AddressDTO, Address>(message.Address);
            //var place = new Place(message.Name, address);
            var place = new Place(message.Name, message.Address.No, message.Address.Street,
                                     message.Address.City, message.Address.State,
                                     message.Address.Country, message.Address.ZipCode);

            this._placeRepository.Add(place);

            return await _placeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CreatePlaceIdentifiedCommandHandler : IdentifiedCommandHandler<CreatePlaceCommand, bool>
    {
        public CreatePlaceIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            // Ignore duplicate requests for creating order.
            return true;
        }
    }
}
