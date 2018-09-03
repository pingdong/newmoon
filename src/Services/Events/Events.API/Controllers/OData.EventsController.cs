using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc.OData;
using PingDong.Newmoon.Events.Service.Commands;
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

        // POST api/v1/events/attendee
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events/attendee")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAttendee([FromHeader(Name = "x-requestid")] string requestId, [FromBody]AddAttendeeCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // DELETE api/v1/events/attendee
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events/attendee")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAttendee([FromHeader(Name = "x-requestid")] string requestId, [FromBody]RemoveAttendeeCommand command)
        {
            return await CommandDispatchAsync(requestId, command);

        }

        // POST api/v1/events
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]CreateEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // PUT api/v1/events
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]UpdateEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // POST api/v1/events/approve
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events/approve")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ApproveEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]ApproveEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // POST api/v1/events/cancel
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events/cancel")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]CancelEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // POST api/v1/events/confirm
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events/confirm")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]ConfirmEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // POST api/v1/events/start
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events/start")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> StartEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]StartEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // POST api/v1/events/end
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("api/v1/odata/events/end")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EndEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]EndEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }
    }
}