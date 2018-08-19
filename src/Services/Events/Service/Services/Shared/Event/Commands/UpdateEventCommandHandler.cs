using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Service.Models;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IEventRepository eventRepository, IPlaceRepository placeRepository, IMapper mapper)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _placeRepository = placeRepository ?? throw new ArgumentNullException(nameof(placeRepository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateEventCommand message, CancellationToken cancellationToken)
        {
            // Check place
            var place = await this._placeRepository.FindByNameAsync(message.Place.Name);
            if (place == null)
            {
                // TODO: Value Object
                //var address = _mapper.Map<AddressDTO, Address>(message.Place.Address);
                //var updatePlace = new Place(message.Place.Name, address);
                var updatePlace = new Place(message.Place.Name, message.Place.Address.No, message.Place.Address.Street,
                    message.Place.Address.City, message.Place.Address.State,
                    message.Place.Address.Country, message.Place.Address.ZipCode);

                place = await this._placeRepository.Add(updatePlace);
            }

            // Create Event
            var evt = await this._eventRepository.GetByIdAsync(message.Id);
            if (evt == null)
                return false;

            evt.UpdateDetail(message.Name, message.StartTime, message.EndTime);
            evt.ChangePlace(place.Id);

            return await _eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class UpdateEventIdentifiedCommandHandler : IdentifiedCommandHandler<UpdateEventCommand, bool>
    {
        public UpdateEventIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}

