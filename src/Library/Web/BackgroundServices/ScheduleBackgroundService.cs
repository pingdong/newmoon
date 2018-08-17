using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace PingDong.Web
{
    /// <summary>
    /// A schedule background service
    /// </summary>
    public abstract class ScheduleBackgroundService : BackgroundService
    {
        // For schedule task
        private readonly CrontabSchedule _schedule;
        private readonly int _interval;
        private DateTime _nextRun;

        /// <summary>
        /// Scheduled Background Service
        /// </summary>
        /// <param name="schedule">Schedule in cron expression</param>
        /// <param name="interval">Schedule checking interval in seconds, the interval has to be less than schedule.</param>
        protected ScheduleBackgroundService(string schedule, int interval)
        {
            _schedule = CrontabSchedule.Parse(schedule);
            _interval = interval * 1000;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);

            do
            {
                if (DateTime.Now > _nextRun)
                {
                    await ProcessAsync(stoppingToken).ConfigureAwait(false);

                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }

                await Task.Delay(_interval, stoppingToken);

            } while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task ProcessAsync(CancellationToken stoppingToken);
    }
}
