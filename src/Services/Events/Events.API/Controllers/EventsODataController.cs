using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Service.Queries;
using PingDong.Web.Http;

namespace PingDong.Newmoon.Events.Controllers
{
    /// <summary>
    /// Ping Controller
    /// </summary>
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