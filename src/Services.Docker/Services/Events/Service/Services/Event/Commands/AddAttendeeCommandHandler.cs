using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Commands
{
    public class AddAttendeeCommandHandler : IRequestHandler<AddAttendeeCommand, bool>
    {
        private readonly IEventRepository _eventRepository;

        public AddAttendeeCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task<bool> Handle(AddAttendeeCommand message, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(message.EventId);
            evt.AddAttendee(new Attendee(message.Identity, message.FirstName, message.LastName));

            return await _eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class AddAttendeeIdentifiedCommandHandler : IdentifiedCommandHandler<AddAttendeeCommand, bool>
    {
        public AddAttendeeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            // Ignore duplicate requests for creating order.
            return true;                
        }
    }
}
