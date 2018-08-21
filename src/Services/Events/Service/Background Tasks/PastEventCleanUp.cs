using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PingDong.AspNetCore.Hosting;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Service.Commands;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Service.BackgroundTasks
{
    /// <summary>
    /// Mark all overdue unconfirmed events finished
    ///
    /// Just for demo the usage of Background Task
    /// </summary>
    public class PastEventCleanUp : ScheduleBackgroundService
    {
        private readonly IEventQuery _query;
        private readonly IMediator _mediator;

        public PastEventCleanUp(IEventQuery query, IMediator mediator) 
            : base("0 0 * * *", 30)
        {
            _query = query;
            _mediator = mediator;
        }

        protected override async Task ProcessAsync(CancellationToken stoppingToken)
        { 
            var events = await _query.GetAllAsync();

            foreach (var evt in events)
            {
                // Overdue and unapproved
                if (evt.StatusId == EventStatus.Created.Id && evt.StartTime < DateTime.Now)
                {
                    var cmd = new EndEventCommand(evt.Id, evt.Name);

                    await _mediator.Send(cmd, stoppingToken);
                }
            }
        }
    }
}
