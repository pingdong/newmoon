using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Controllers
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [ApiController]
    [Route("api/v1/events/odata")]
    [Produces("application/json")]
    public class EventsODataController : BaseODataController<EventSummary>
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="query"></param>
        public EventsODataController(ILogger<EventsController> logger, IEventQuery query) 
            : base(logger, query)
        {
        }
    }
}