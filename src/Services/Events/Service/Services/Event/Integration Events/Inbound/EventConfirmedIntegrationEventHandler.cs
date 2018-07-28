using System.Threading.Tasks;
using PingDong.EventBus.Abstractions;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.IntegrationEvents
{
    public class EventConfirmedIntegrationEventHandler : IIntegrationEventHandler<EventConfirmedIntegrationEvent>
    {
        private readonly IEventRepository _repository;

        public EventConfirmedIntegrationEventHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event">       
        /// </param>
        /// <returns></returns>
        public async Task Handle(EventConfirmedIntegrationEvent @event)
        {
            var eventToUpdate = await _repository.GetByIdAsync(@event.EventId);
            eventToUpdate.Confirm();

            await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
