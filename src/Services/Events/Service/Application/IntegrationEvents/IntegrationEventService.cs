using System;
using System.Data.Common;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using PingDong.EntityFrameworkCore;
using PingDong.EventBus.Abstractions;
using PingDong.EventBus.Events;
using PingDong.EventBus.Services;

namespace PingDong.Newmoon.Events.Service.IntegrationEvents
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly EventBusLogServiceDbContext _context;
        private readonly IEventBusLogService _logService;

        public IntegrationEventService(IEventBus eventBus, EventBusLogServiceDbContext context, 
                    Func<DbConnection, IEventBusLogService> eventBusLogServiceFactory)
        {
            if (eventBusLogServiceFactory == null)
                throw new ArgumentNullException(nameof(eventBusLogServiceFactory));

            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            
            _logService = eventBusLogServiceFactory(_context.Database.GetDbConnection());
        }

        public async Task PublishAsync(IntegrationEvent evt)
        {
            await SaveEventAndContextChangesAsync(evt);

            _eventBus.Publish(evt);

            await _logService.MarkAsPublishedAsync(evt);
        }

        private async Task SaveEventAndContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency   
            await ResilientTransaction.New(_context)
                .ExecuteAsync(async () => {
                    // Achieving atomicity between original database operation and the EventBusLog through a local transaction
                    await _context.SaveChangesAsync();
                    await _logService.SaveAsync(evt, _context.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }
}
