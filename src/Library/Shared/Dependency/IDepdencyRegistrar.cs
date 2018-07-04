using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PingDong.Application.Dependency
{
    /// <summary>
    /// IDepdencyRegistrar
    /// </summary>
    public interface IDepdencyRegistrar
    {
        DependecyType RegisterType { get; }

        /// <summary>
        /// Injecting services
        /// </summary>
        /// <param name="services">The Service Collection that will register to</param>
        /// <param name="configuration">configuration</param>
        /// <param name="logger">Logger</param>
        void Inject(IServiceCollection services, IConfiguration configuration, ILogger logger);
    }
}
