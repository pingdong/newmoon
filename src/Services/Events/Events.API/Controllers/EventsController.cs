using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc;
using PingDong.Newmoon.Events.Service.Commands;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Controllers
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class EventsController : BaseController
    {
        private readonly IEventQuery _query;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mediator"></param>
        /// <param name="query"></param>
        public EventsController(ILogger<EventsController> logger, IMediator mediator, IEventQuery query) 
            : base(logger, mediator)
        {
            _query = query;
        }

        // GET api/v1/events
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [Route("")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEvents()
        {
            return await GetAllAsync(_query);
        }

        // GET api/v1/events/id
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("{id:int}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetEvent(int id)
        {
            return await GetByIdAsync(_query, id);
        }

        // POST api/v1/events/attendee
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("attendee")]
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

        [Route("attendee")]
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

        [Route("")]
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

        [Route("")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]UpdateEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // POST api/v1/events/cancel
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>

        [Route("cancel")]
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

        [Route("confirm")]
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

        [Route("start")]
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

        [Route("end")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EndEvent([FromHeader(Name = "x-requestid")] string requestId, [FromBody]EndEventCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }
    }
}