﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PingDong.Newmoon.Events.Api.IntegrationTest
{
    public class EventTestsStartup : Startup
    {
        public EventTestsStartup(IConfiguration config, ILogger<Startup> logger, IHostingEnvironment env) : base(config, logger, env)
        {
        }
    }
}
