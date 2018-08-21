using System.Threading.Tasks;

using PingDong.EventBus;

namespace PingDong.Newmoon.Events.Service.IntegrationEvents
{
    public class EmptyIntegrationEventService : IIntegrationEventService
    {
        public Task PublishAsync(IntegrationEvent evt)
        {
            return Task.CompletedTask;
        }
    }
}
