using Autofac;
using Microsoft.Extensions.Hosting;
using PingDong.Application.Dependency;
using PingDong.Newmoon.Events.Service.BackgroundTasks;

namespace PingDong.Newmoon.Events.Service
{
    public class EventsBackgroundTaskRegistrar : BackgroundTaskRegistrar
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Background Tasks
            builder.RegisterType<PastEventCleanUp>().As<IHostedService>().SingleInstance();
        }
    }
}
