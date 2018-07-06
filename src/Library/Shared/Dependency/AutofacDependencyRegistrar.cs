using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PingDong.Application.Dependency
{
    public class AutofacDependencyRegistrar : Module
    {

        public AutofacDependencyRegistrar(DependecyType dependecyType)
        {
            RegisterType = dependecyType;
        }

        public DependecyType RegisterType { get; }

        public IConfiguration Configuration { get; set; }

        public ILogger Logger { get; set; }
    }
}
