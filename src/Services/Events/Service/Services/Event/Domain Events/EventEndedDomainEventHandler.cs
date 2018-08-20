using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Core.Events;

namespace PingDong.Newmoon.Events.Service.DomainEvents
{
    public class EventEndedDomainEventHandler : INotificationHandler<EventEndedDomainEvent>
    {
        private readonly IPlaceRepository _repository;
        private readonly ILoggerFactory _logger;

        public EventEndedDomainEventHandler(IPlaceRepository repository, ILoggerFactory logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(EventEndedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (!domainEvent.Event.PlaceId.HasValue)
                return;

            var place = await _repository.GetByIdAsync(domainEvent.Event.PlaceId.Value);
            place.Disengage();

            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
