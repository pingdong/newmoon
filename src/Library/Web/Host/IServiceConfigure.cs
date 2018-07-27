using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace PingDong.AspNetCore
{
    public interface IServiceConfigure
    {
        void Config(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory);
    }
}
