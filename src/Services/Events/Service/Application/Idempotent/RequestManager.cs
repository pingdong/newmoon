using System;
using System.Threading.Tasks;
using PingDong.DomainDriven.Core;
using PingDong.DomainDriven.Service;
using PingDong.Newmoon.Events.Core.Exceptions;
using PingDong.Newmoon.Events.Infrastructure;

namespace PingDong.Newmoon.Events.Service.Commands.Idempotent
{
    public class RequestManager : IRequestManager
    {
        private readonly EventContext _context;

        public RequestManager(EventContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CheckExistAsync(Guid id)
        {
            var request = await _context.FindAsync<ClientRequest>(id);

            return request != null;
        }

        public async Task CreateRequestRecordAsync<T>(Guid id)
        { 
            var exists = await CheckExistAsync(id);

            var request = exists 
                            ? throw new EventDomainException($"Request with {id} already exists") 
                            : new ClientRequest
                                {
                                    Id = id,
                                    Name = typeof(T).Name,
                                    Time = DateTime.UtcNow
                                };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }
    }
}
