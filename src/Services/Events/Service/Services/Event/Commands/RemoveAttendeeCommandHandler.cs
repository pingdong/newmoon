using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class RemoveAttendeeCommandHandler : IRequestHandler<RemoveAttendeeCommand, bool>
    {
        private readonly IEventRepository _eventRepository;

        public RemoveAttendeeCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task<bool> Handle(RemoveAttendeeCommand message, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(message.EventId);
            if (evt == null)
                return false;

            var attendee = evt.Attendees.FirstOrDefault(a =>
                a.Identity.Equals(message.Identity, StringComparison.InvariantCultureIgnoreCase));
            if (attendee == null)
                return false;
            
            evt.RemoveAttendee(message.Identity);

            return await _eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class RemoveAttendeeIdentifiedCommandHandler : IdentifiedCommandHandler<RemoveAttendeeCommand, bool>
    {
        public RemoveAttendeeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            // Ignore duplicate requests for creating order.
            return true;
        }
    }
}
