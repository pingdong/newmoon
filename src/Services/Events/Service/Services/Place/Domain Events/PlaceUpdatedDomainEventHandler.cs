using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Core.Events;

namespace PingDong.Newmoon.Events.Service.DomainEvents
{
    public class PlaceUpdatedDomainEventHandler : INotificationHandler<PlaceUpdatedDomainEvent>
    {
        private readonly IPlaceRepository _repository;
        private readonly ILoggerFactory _logger;

        public PlaceUpdatedDomainEventHandler(IPlaceRepository repository, ILoggerFactory logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PlaceUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
        }
    }
}
