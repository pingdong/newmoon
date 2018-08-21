using Microsoft.Azure.ServiceBus;
using System;

namespace PingDong.EventBus.Azure
{
    public interface IServiceBusPersisterConnection : IDisposable
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient CreateModel();
    }
}