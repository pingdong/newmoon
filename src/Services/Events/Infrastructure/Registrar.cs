using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Application.Dependency;
using PingDong.Newmoon.Events.Infrastructure;

namespace PingDong.Newmoon.Events
{
    /// <summary>
    /// Register in ASP.Net Dependency Container
    /// </summary>
    public class Registrar : IDepdencyRegistrar
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
                    .AddDbContext<EventContext>(options =>
                        {
                            options.UseSqlServer(connectionString,
                                sqlServerOptionsAction: sqlOptions =>
                                {
                                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                });
                        }
                        // , ServiceLifetime.Scoped
                        // Default lifetime
                        // Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                    );
        }
    }
}
