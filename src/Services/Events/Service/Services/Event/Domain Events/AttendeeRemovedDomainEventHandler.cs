using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Core.Events;

namespace PingDong.Newmoon.Events.Service.DomainEvents
{
    public class AttendeeRemovedDomainEventHandler : INotificationHandler<AttendeeRemovedDomainEvent>
    {
        private readonly IEventRepository _repository;
        private readonly ILoggerFactory _logger;

        public AttendeeRemovedDomainEventHandler(IEventRepository repository, ILoggerFactory logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(AttendeeRemovedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
