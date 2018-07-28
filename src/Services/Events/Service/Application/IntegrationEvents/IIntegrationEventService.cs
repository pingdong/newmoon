using System.Threading.Tasks;
using PingDong.EventBus.Events;

namespace PingDong.Newmoon.Events.Service.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishAsync(IntegrationEvent evt);
    }
}
