using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Integration.Test.Infrastructure;

namespace PingDong.Newmoon.Events.Integration.Test
{
    public class TestsStartup : Startup
    {
        public TestsStartup(IConfiguration config, ILogger<Startup> logger, IHostingEnvironment env)
            : base(config, logger, env)
        {
        }

        protected override void UseAuth(IApplicationBuilder app)
        {
            if (Configuration["isTest"] == bool.TrueString.ToLowerInvariant())
            {
                app.UseMiddleware<AutoAuthorizeMiddleware>();
            }
            else
            {
                base.UseAuth(app);
            }
        }
    }
}
