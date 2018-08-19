using PingDong.EventBus.Abstractions;

namespace PingDong.EventBus.Abstractions
{
    public interface IEventBusSubscription
    {
        void Subscribe(IEventBus eventBus);
    }
}
