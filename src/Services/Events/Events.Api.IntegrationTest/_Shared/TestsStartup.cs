using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Infrastructure;

namespace PingDong.Newmoon.Events.Shared
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration config, ILogger<Startup> logger, IHostingEnvironment env)
            : base(config, logger, env)
        {
        }

        protected override void UseAuth(IApplicationBuilder app)
        {
            app.UseMiddleware<AutoAuthorizeMiddleware>();
        }
    }
}
