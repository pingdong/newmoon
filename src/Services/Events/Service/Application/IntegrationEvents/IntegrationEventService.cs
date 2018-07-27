using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
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

        public IntegrationEventService(IEventBus eventBus, IEventBusLogService logService, EventBusLogServiceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public async Task PublishAsync(IntegrationEvent evt)
        {
            await SaveEventAndContextChangesAsync(evt);

            _eventBus.Publish(evt);

            await _logService.MarkAsPublishedAsync(evt);
        }

        private async Task SaveEventAndContextChangesAsync(IntegrationEvent evt)
        {       
            await ResilientTransaction.New(_context)
                    .ExecuteAsync(async () => {
                        // Achieving atomicity between original ordering database operation and the IntegrationEventLog thanks to a local transaction
                        await _context.SaveChangesAsync();
                        await _logService.SaveAsync(evt, _context.Database.CurrentTransaction.GetDbTransaction());
                    });
        }
    }
}
