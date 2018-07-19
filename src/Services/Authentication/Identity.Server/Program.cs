using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PingDong.Newmoon.IdentityServer.Authentication;
using PingDong.Web.AspNetCore.Hosting;
using Serilog;

namespace PingDong.Newmoon.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHost(args)
                            // Asp.Net Core User Management
                            .MigrateDbContext<ApplicationDbContext>((context, services) => { });

            host.Run();
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                        .UseKestrel()
                        // Application Configure
                        .ConfigureAppConfiguration((builderContext, config) =>
                        {
                            var env = builderContext.HostingEnvironment;

                            config.SetBasePath(env.ContentRootPath);

                            if (env.IsDevelopment())
                            {
                                config.AddUserSecrets<Startup>();
                            }

                            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                                  .AddCommandLine(args)
                                  .AddEnvironmentVariables();
                        })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        // HealthChecks initialise
                        .UseHealthChecks("/health")
                        // Application Configure
                        .UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); })
                        .UseApplicationInsights()
                        .UseStartup<Startup>()
                .UseUrls("http://localhost:5001")
                        .Build();
    }
}
