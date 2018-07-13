﻿using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Infrastructure;
using PingDong.QualityTools.Core;
using PingDong.Web.AspNetCore.Hosting;

namespace PingDong.Newmoon.Events.Integration.Test
{ 
    public class ScenarioBase : IDisposable
    {
        public TestServer CreateServer(string baseDir)
        {
            var cfg = new ConfigurationBuilder()
                .SetBasePath(baseDir)
                .AddJsonFile("Settings.json")
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()
                                        .UseContentRoot(baseDir)
                                        .UseEnvironment("Stagging")
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

        public static class Events
        {
            public static class Get
            {
                public static string All = "api/v1/events";

                public static string ById(int id)
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
                public static string ConfirmEvent = "api/v1/events/confirm";
                public static string StartEvent = "api/v1/events/start";
                public static string EndEvent = "api/v1/events/end";
            }
        }

        public void Dispose()
        {
            // Clean up the test environment

            // Removing physic db file
            InMemoryDbTestHelper.CleanUp();
        }
    }
}