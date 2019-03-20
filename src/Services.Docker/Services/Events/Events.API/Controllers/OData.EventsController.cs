using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc.OData;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Controllers
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [ODataRoutePrefix("events")]
    public class EventsODataController : BaseODataController
    {
        private readonly IEventQuery _eventQuery;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mediator"></param>
        /// <param name="eventQuery"></param>
        public EventsODataController(ILogger<EventsODataController> logger, IMediator mediator, IEventQuery eventQuery)
            : base(logger, mediator)
        {
            _eventQuery = eventQuery;
        }

        /// <summary>
        /// Get Events
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [ODataRoute("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _eventQuery.GetAllAsync());
        }

        /// <summary>
        /// Get specific event by its id
        /// </summary>
        /// <param name="id">event id</param>
        /// <returns></returns>
        [EnableQuery]
        [ODataRoute("({id})")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEvent([FromODataUri] int id)
        {
            var evt = await _eventQuery.GetByIdAsync(id);

            return evt == null ? (IActionResult) NotFound() : Ok(evt);
        }
    }
}