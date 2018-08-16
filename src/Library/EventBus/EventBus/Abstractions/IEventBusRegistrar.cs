using PingDong.EventBus.Abstractions;

namespace PingDong.EventBus
{
    public interface IEventBusRegistrar
    {
        void Subscribe(IEventBus eventBus);
    }
}
