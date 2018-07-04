using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Core.Events;

namespace PingDong.Newmoon.Events.Service.DomainEvents
{
    public class EventStartedDomainEventHandler : INotificationHandler<EventStartedDomainEvent>
    {
        private readonly IEventRepository _repository;
        private readonly ILoggerFactory _logger;

        public EventStartedDomainEventHandler(IEventRepository repository, ILoggerFactory logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(EventStartedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
        }
    }
}
