﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.Application.Dependency;

namespace PingDong.Newmoon.Events.Infrastructure
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
            var connectionString = configuration["SqlServer_ConnectionString"];

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<EventContext>(options =>
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
        }
    }
}
