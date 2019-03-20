using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PingDong.DomainDriven.Service;
using PingDong.Linq;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Service.Commands.Models;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, bool>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IEventRepository eventRepository, IPlaceRepository placeRepository, IMapper mapper)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _placeRepository = placeRepository ?? throw new ArgumentNullException(nameof(placeRepository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateEventCommand message, CancellationToken cancellationToken)
        {
            // Create Event
            var evt = new Event(message.Name, message.StartTime, message.EndTime);

            // Set Place
            var place = await this._placeRepository.FindByNameAsync(message.Place?.Name);
            if (place == null)
            {
                // TODO: Value Object
                //var address = _mapper.Map<AddressDTO, Address>(message.Place.Address);
                //var newPlace = new Place(message.Place.Name, address);
                var newPlace = new Place(message.Place.Name, message.Place.Address.No, message.Place.Address.Street,
                    message.Place.Address.City, message.Place.Address.State,
                    message.Place.Address.Country, message.Place.Address.ZipCode);

                place = await this._placeRepository.Add(newPlace);
            }

            evt.ChangePlace(place.Id);

            // Save attendee
            if (!message.Attendees.IsNullOrEmpty())
            {
                foreach (var attendee in message.Attendees)
                {
                    var att = _mapper.Map<AttendeeDTO, Attendee>(attendee);
                    evt.AddAttendee(att);
                }
            }

            this._eventRepository.Add(evt);

            return await _eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CreateEventIdentifiedCommandHandler : IdentifiedCommandHandler<CreateEventCommand, bool>
    {
        public CreateEventIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}

