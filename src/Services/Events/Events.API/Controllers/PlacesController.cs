using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc;
using PingDong.Newmoon.Events.Service.Commands;
using PingDong.Newmoon.Events.Service.Queries.Rest;

namespace PingDong.Newmoon.Events.Controllers.Rest
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class PlacesController : BaseController
    {
        private readonly IPlaceQuery _query;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mediator"></param>
        /// <param name="query"></param>
        public PlacesController(ILogger<PlacesController> logger, IMediator mediator, IPlaceQuery query) 
            : base(logger, mediator)
        {
            _query = query;
        }

        // GET api/places
        /// <summary>
        /// Get all places
        /// </summary>
        /// <returns>Return all places</returns>

        [Route("")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPlaces()
        {
            return await GetAllAsync(_query);
        }

        // POST api/Places
        /// <summary>
        /// Create a new place
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePlace([FromHeader(Name = "x-requestid")] string requestId, [FromBody]CreatePlaceCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }

        // PUT api/Places
        /// <summary>
        /// Update specified place
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdatePlace([FromHeader(Name = "x-requestid")] string requestId, [FromBody]UpdatePlaceCommand command)
        {
            return await CommandDispatchAsync(requestId, command);
        }
    }
}