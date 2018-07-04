using Autofac.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PingDong.Newmoon.Events
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args">args</param>
        public static void Main(string[] args)
        {
            // Serilog Initialize
            //   It is only needed for logging WebHost initialisation
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                                    .Build();
            Log.Logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(configuration)
                                .CreateLogger();

            try
            {
                Log.Information("Starting web host for Event Services");

                var webhost = BuildWebHost(args);
                    
                webhost.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "'Event Service' Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Web host building
        /// </summary>
        /// <param name="args">args</param>
        /// <returns>Web host</returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                            // Application Configure
                            .ConfigureAppConfiguration((builderContext, config) =>
                            {
                                var env = builderContext.HostingEnvironment;

                                config.SetBasePath(env.ContentRootPath)
                                    .AddEnvironmentVariables()
                                    .AddJsonFile("appsettings.json",
                                        optional: true,
                                        reloadOnChange: true)
                                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                        optional: true);
                            })
                            // Autofac intialise
                            .ConfigureServices(services => services.AddAutofac())
                            // HealthChecks initialise
                            .UseHealthChecks("/health")
                            // Application Insights initialise
                            .UseApplicationInsights()
                            // Serilog initialise
                            .UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); })
                            // Starting
                            .UseStartup<Startup>()
                            .Build();
        }
    }
}