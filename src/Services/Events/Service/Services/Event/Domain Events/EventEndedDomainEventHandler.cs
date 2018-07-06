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
        private readonly IEventRepository _repository;
        private readonly ILoggerFactory _logger;

        public EventEndedDomainEventHandler(IEventRepository repository, ILoggerFactory logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(EventEndedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
