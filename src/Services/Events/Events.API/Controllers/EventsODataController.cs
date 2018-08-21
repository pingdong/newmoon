using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc.Rest;
using PingDong.Newmoon.Events.Service.Queries.Models;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Controllers.Rest
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("api/v1/events/odata")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class EventsODataController : BaseODataController<EventSummary>
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="query"></param>
        public EventsODataController(ILogger<EventsODataController> logger, IEventQuery query) 
            : base(logger, query)
        {
        }
    }
}