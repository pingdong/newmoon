using System;
using System.Collections.Generic;
using PingDong.EventBus.Abstractions;

namespace PingDong.EventBus.Subscription
{
    public interface IEventBusSubscriptionsManager
    {
        Type GetEventTypeByName(string eventName);
        string GetEventKey<T>();
        bool IsEmpty { get; }
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        bool HasSubscriptionsForEvent(string eventName);

        IEnumerable<InMemoryEventBusSubscriptionsManager.SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<InMemoryEventBusSubscriptionsManager.SubscriptionInfo> GetHandlersForEvent(string eventName);

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>;
        void RemoveSubscription<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>;
        void AddDynamicSubscription<THandler>(string eventName) where THandler : IDynamicIntegrationEventHandler;
        void RemoveDynamicSubscription<THandler>(string eventName) where THandler : IDynamicIntegrationEventHandler;
        void Clear();
    }
}