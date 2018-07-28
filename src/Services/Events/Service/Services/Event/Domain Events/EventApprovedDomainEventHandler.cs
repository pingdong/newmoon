using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Core.Events;
using PingDong.Newmoon.Events.Service.IntegrationEvents;

namespace PingDong.Newmoon.Events.Service.DomainEvents
{
    public class EventApprovedDomainEventHandler : INotificationHandler<EventApprovedDomainEvent>
    {
        private readonly IPlaceRepository _repository;
        private readonly IIntegrationEventService _eventBus;

        public EventApprovedDomainEventHandler(IPlaceRepository repository, ILoggerFactory logger, IIntegrationEventService eventBus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventBus = eventBus;
        }

        public async Task Handle(EventApprovedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var dv = domainEvent.Event;
            var place = await _repository.GetByIdAsync(dv.PlaceId.Value);

            var evt = new EventAwaitingConfirmIntegrationEvent(dv.Id, dv.Name, place.Name, dv.StartTime, dv.EndTime);
            await _eventBus.PublishAsync(evt);
        }
    }
}
