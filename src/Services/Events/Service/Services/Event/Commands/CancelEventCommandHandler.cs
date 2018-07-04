using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand, bool>
    {
        private readonly IEventRepository _eventRepository;

        public CancelEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task<bool> Handle(CancelEventCommand message, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(message.EventId);

            if (evt.Name.Equals(message.EventName, StringComparison.InvariantCultureIgnoreCase))
                return false;

            evt.Cancel();

            return await _eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CancelEventIdentifiedCommandHandler : IdentifiedCommandHandler<CancelEventCommand, bool>
    {
        public CancelEventIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}
