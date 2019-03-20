using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class ApproveEventCommandHandler : IRequestHandler<ApproveEventCommand, bool>
    {
        private readonly IEventRepository _eventRepository;

        public ApproveEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task<bool> Handle(ApproveEventCommand message, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(message.EventId);

            if (!evt.Name.Equals(message.EventName, StringComparison.InvariantCultureIgnoreCase))
                return false;

            evt.Approve();

            return await _eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class ApproveEventIdentifiedCommandHandler : IdentifiedCommandHandler<ApproveEventCommand, bool>
    {
        public ApproveEventIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}
