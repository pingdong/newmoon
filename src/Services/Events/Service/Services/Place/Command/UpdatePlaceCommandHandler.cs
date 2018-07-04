using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, bool>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;

        public UpdatePlaceCommandHandler(IPlaceRepository placeRepository, IMapper mapper)
        {
            _placeRepository = placeRepository ?? throw new ArgumentNullException(nameof(placeRepository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePlaceCommand message, CancellationToken cancellationToken)
        {
            var place = await this._placeRepository.GetByIdAsync(message.Id);
            if (place == null)
                return false;

            // TODO: Value Object
            //var address = _mapper.Map<AddressDTO, Address>(message.Address);
            //place.Update(message.Name, address);
            place.Update(message.Name, message.Address.No, message.Address.Street,
                message.Address.City, message.Address.State,
                message.Address.Country, message.Address.ZipCode);

            return await _placeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class UpdatePlaceIdentifiedCommandHandler : IdentifiedCommandHandler<UpdatePlaceCommand, bool>
    {
        public UpdatePlaceIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            // Ignore duplicate requests for creating order.
            return true;
        }
    }
}
