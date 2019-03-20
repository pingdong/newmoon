using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace PingDong.Newmoon.Events.Infrastructure
{
    public class EventContextSeed
    {
        public async Task SeedAsync(EventContext context, ILogger<EventContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(EventContextSeed));

            await policy.ExecuteAsync(async () =>
                {
                    using (context)
                    {
                        context.Database.Migrate();

                        // Create seed data here

                        await context.SaveChangesAsync();
                    }
                });
        }
        
        private AsyncRetryPolicy CreatePolicy(ILogger<EventContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}
