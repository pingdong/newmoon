using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PingDong.Newmoon.IdentityServer.Data;
using PingDong.Web.AspNetCore.Hosting;
using Serilog;

namespace PingDong.Newmoon.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build()
                            .MigrateDbContext<ApplicationDbContext>((context, services) => { });
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
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
                .UseStartup<Startup>();
    }
}
