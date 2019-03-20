using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Hosting;
using PingDong.Newmoon.Events.Infrastructure;
using PingDong.QualityTools.Infrastrucutre.SqlServer;

namespace PingDong.Newmoon.Events.Shared
{
    public class EventsWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        private readonly string _databases = Guid.NewGuid().ToString();

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var cfg = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"{Directory.GetCurrentDirectory()}\\..\\..\\settings.json", optional: false)
                            .AddInMemoryCollection(InMemoryDbTestHelper.BuildDatabaseConnectionSetting(_databases))
                            .AddInMemoryCollection(new Dictionary<string, string>
                                {
                                    { "IdentityServiceUri", "192.168.5.5" },
                                    { "EventsServiceUri", "192.168.5.10" },
                                    { "isTest", "True" }
                                })
                            .Build();

            return WebHost.CreateDefaultBuilder()
                            .UseEnvironment(EnvironmentName.Development)
                            .UseConfiguration(cfg)
                            .UseStartup<TestStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseSolutionRelativeContentRoot(Directory.GetCurrentDirectory());
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var testServer = new TestServer(builder);

            testServer.Host
                .MigrateDbContext<EventContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<EventContextSeed>>();

                    new EventContextSeed()
                        .SeedAsync(context, logger)
                        .Wait();
                });

            return testServer;
        }

        protected override void Dispose(bool disposing)
        {
            // Clean up the test environment

            // Removing physical db file
            InMemoryDbTestHelper.CleanUp(_databases);

            base.Dispose(disposing);
        }
    }
}
