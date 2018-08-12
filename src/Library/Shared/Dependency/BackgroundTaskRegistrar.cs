using Autofac;
using Microsoft.Extensions.Logging;

namespace PingDong.Application.Dependency
{
    public class BackgroundTaskRegistrar : Module
    {
        public ILogger Logger { get; set; }
    }
}
