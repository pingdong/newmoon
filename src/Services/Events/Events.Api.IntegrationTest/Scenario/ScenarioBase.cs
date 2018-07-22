using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Hosting;
using PingDong.Newmoon.Events.Infrastructure;
using PingDong.QualityTools.Infrastrucutre.SqlServer;

namespace PingDong.Newmoon.Events.Integration.Test
{ 
    public class ScenarioBase : IDisposable
    {
        public TestServer CreateServer(string baseDir)
        {
            var cfg = new ConfigurationBuilder()
                            .SetBasePath(baseDir)
                            .AddJsonFile("Settings.json")
                            .AddInMemoryCollection(InMemoryDbTestHelper.BuildDatabaseConnectionSetting(_dbName))
                            .AddInMemoryCollection(new Dictionary<string, string>
                                {
                                    { "isTest", "true" }
                                })
                            .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()
                                        .UseContentRoot(baseDir)
                                        .UseEnvironment(EnvironmentName.Development)
                                        .UseConfiguration(cfg)
                                        .UseStartup<TestStartup>();

            var testServer = new TestServer(webHostBuilder);

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

        private readonly string _dbName = Guid.NewGuid().ToString();

        public void Dispose()
        {
            // Clean up the test environment

            // Removing physic db file
            InMemoryDbTestHelper.CleanUp(_dbName);
        }
    }
}
