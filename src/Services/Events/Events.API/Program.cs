using Autofac.Extensions.DependencyInjection;
using Serilog;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Infrastructure;
using PingDong.Web.AspNetCore.Hosting;

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
            //   It is only needed to support logging in the initialisation.

            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env}.json", optional: true)
            //    .Build();
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            //try
            //{
            //    Log.Information("Starting web host for Event Services");

            //    var webhost = BuildWebHost(args).Build();

            //    webhost.Run();
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "'Event Service' Host terminated unexpectedly");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}

            var host = BuildWebHost(args).Build();
            host.MigrateDbContext<EventContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<EventContextSeed>>();

                    new EventContextSeed()
                            .SeedAsync(context, logger)
                            .Wait();
                });
            // If seeing is not needed
            //host.MigrateDbContext<EventContext>((_, __) => { });
            host.Run();
        }

        /// <summary>
        /// Web host building
        /// </summary>
        /// <param name="args">args</param>
        /// <returns>Web host</returns>
        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                // Application Configure
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var env = builderContext.HostingEnvironment;

                    config.SetBasePath(Directory.GetCurrentDirectory());

                    if (env.IsDevelopment())
                    {
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
                        config.AddUserSecrets<Startup>();
                    }
                        
                    config
                        // Application related setting
                        //   for example: log setting
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        // If the same settings in appsettings.json needs to be replaced in development environment, write here
                        //   for example: different logging setting
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)     
                        
                        // Environment-aware settings
                        //   for example: external service uri
                        .AddCommandLine(args)
                        .AddEnvironmentVariables();

                    config.Build();

                    // Secrets should save in vault in production environment
                    //   config need to be built to retrieve Vault information
                    //  
                    //      DbConnectionString in production
                    //
                    //if (env.IsProduction())
                    //{
                    //    var vault = new ConfigurationBuilder();
                    //    vault.AddAzureKeyVault(
                    //        $"https://{config["AzureVault:VaultName"]}.vault.azure.net/",
                    //        config["AzureVault:ClientId"],
                    //        config["AzureVault:ClientSecret"]);

                    //    var vaultConfig = vault.Build();

                    //    config.AddConfiguration(vaultConfig);
                    //}
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
                .UseStartup<Startup>();
    }
}