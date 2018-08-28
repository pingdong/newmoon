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

namespace PingDong.Newmoon.Events.Shared
{ 
    public class ScenarioBase : IDisposable
    {
        public TestServer CreateServer(string baseDir)
        {
            var cfg = new ConfigurationBuilder()
                            .SetBasePath(baseDir)
                            .AddJsonFile($"{baseDir}Events\\settings.json")
                            .AddInMemoryCollection(InMemoryDbTestHelper.BuildDatabaseConnectionSetting(_dbName))
                            .AddInMemoryCollection(new Dictionary<string, string>
                                {
                                    { "IdentityServiceUri", "192.168.5.5" },
                                    { "EventBus:Enabled", "False" },
                                    { "isTest", "True" }
                                })
                            .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()
                                        .UseContentRoot(baseDir)
                                        .UseEnvironment(EnvironmentName.Development)
                                        .UseConfiguration(cfg)
                                        .UseStartup<TestsStartup>();

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

        public class Api
        {
            public class RESTful
            {
                public static class Get
                {
                    public static string Events = "api/v1/events";
                    public static string Places = "api/v1/places";

                    public static string EventById(int id)
                    {
                        return $"api/v1/events/{id}";
                    }
                }

                public static class Put
                {
                    public static string UpdateEvent = "api/v1/events";
                }

                public static class Post
                {
                    public static string AddEvent = "api/v1/events";
                    public static string CancelEvent = "api/v1/events/cancel";
                    public static string ApproveEvent = "api/v1/events/approve";
                    public static string ConfirmEvent = "api/v1/events/confirm";
                    public static string StartEvent = "api/v1/events/start";
                    public static string EndEvent = "api/v1/events/end";
                }
            }
            
            public class OData
            {
                public static class Get
                {
                    public static string Events = "api/v1/odata/events";
                    public static string Places = "api/v1/odata/places";
                }
            }
        }

        private readonly string _dbName = Guid.NewGuid().ToString();

        public void Dispose()
        {
            // Clean up the test environment

            // Removing physical db file
            InMemoryDbTestHelper.CleanUp(_dbName);
        }
    }
}
