using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PingDong.Newmoon.Events.Functional.Test
{
    public class TestsStartup : Startup
    {
        public TestsStartup(IConfiguration config, ILogger<Startup> logger, IHostingEnvironment env) 
            : base(config, logger, env)
        {
        }
    }
}
