using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PingDong.AspNetCore.Hosting;
using PingDong.Newmoon.IdentityServer.Authentication;
using Serilog;

namespace PingDong.Newmoon.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    // Application related setting
                    //   for example: log setting
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    // If the same settings in appsettings.json needs to be replaced in development environment, write here
                    //   for example: different logging setting
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    // Some secrets that used in Development could save in UserSecrects
                    //   For example: you probably don't want to share you database credential,
                    //                if you want to share your codes.
                    // https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets
                    //     dotnet user-secrets set "Database:DbPassword" "pass123"
                    //     dotnet user-secrets list
                    //     dotnet user-secrets remove "Database:DbPassword"
                    //     dotnet user-secrets clear
                    //
                    // Or in Visual Studio
                    // Right click on the target project, then click 'Manage User Secrets'
                    .AddUserSecrets<Startup>();

            if (env != "Development")
            {
                var endpoint = Environment.GetEnvironmentVariable("Azure:Vault:Endpoint");
                var clientId = Environment.GetEnvironmentVariable("Azure:Vault:ClientId");
                var clientSecret = Environment.GetEnvironmentVariable("Azure:Vault:ClientSecret");

                if (!string.IsNullOrWhiteSpace(endpoint) && !string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
                {
                    builder.AddAzureKeyVault(
                        $"https://{endpoint}.vault.azure.net/",
                        clientId,
                        clientSecret);
                }

            }

            builder
                    // Environment-aware settings
                    //   for example: external service uri
                    .AddCommandLine(args)
                    .AddEnvironmentVariables();

            var configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host for Event Services");

                var host = BuildWebHost(args, configuration).Build();

                host.MigrateDbContext<ApplicationDbContext>((context, services) => { });

                host.Run();
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

        public static IWebHostBuilder BuildWebHost(string[] args, IConfiguration configuration) =>
            WebHost.CreateDefaultBuilder(args)
                    .CaptureStartupErrors(true)
                    .UseKestrel()
                    // Application Configure
                    .ConfigureAppConfiguration((builderContext, config) =>
                        {
                            // Configuration Source
                            config.AddConfiguration(configuration);
                        })
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    // HealthChecks initialise
                    .UseHealthChecks("/health")
                    .UseApplicationInsights()
                    // Application Configure
                    .UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); })
                    .UseStartup<Startup>();
    }
}
