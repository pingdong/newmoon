using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Application.Dependency;
using PingDong.EventBus.Services;

namespace PingDong.EventBus
{
    /// <summary>
    /// Register in ASP.Net Dependency Container
    /// </summary>
    public class EventBusRegistrar : IDepdencyRegistrar
    {
        public DependecyType RegisterType => DependecyType.Infrastructure;

        /// <summary>
        /// Inject into dependency container
        /// </summary>
        /// <param name="services">container</param>
        /// <param name="configuration">configuration</param>
        /// <param name="loggerFactory"></param>
        public void Inject(IServiceCollection services, IConfiguration configuration, ILogger loggerFactory)
        {
            var connectionString = configuration.GetConnectionString("DefaultDbConnection");

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<EventBusLogServiceDbContext>(options =>
                    {
                        var builder = options.UseSqlServer(connectionString,
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });
                        if (configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
                        {
                            builder.EnableSensitiveDataLogging()
                                // throw an exception when you are evaluating a query in-memory instead of in SQL, for performance
                                .ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning));
                        }
                    }
                    // , ServiceLifetime.Scoped
                    // Default lifetime
                    // Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                );
            
            services.AddTransient<Func<DbConnection, IEventBusLogService>>(sp => (DbConnection c) => new EventBusLogService(c));

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }
    }
}
