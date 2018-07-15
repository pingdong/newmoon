using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using PingDong.DomainDriven.Core;
using PingDong.DomainDriven.Core.Validation;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext _context;
        private readonly IEnumerable<IValidator<Event>> _validators;

        public IUnitOfWork UnitOfWork => _context;

        public EventRepository(EventContext context, IEnumerable<IValidator<Event>> validators)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _validators = validators;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt != null)
            {
                await _context.Entry(evt)
                              .Collection(i => i.Attendees).LoadAsync();
            }

            return evt;
        }

        public Task<Event> Add(Event evt)
        {
            _validators.Validate(evt);

            if (evt.IsTransient())
            {
                evt = _context.Events
                               .Add(evt)
                               .Entity;
            }

            return Task.FromResult(evt);
        }
    }
}
