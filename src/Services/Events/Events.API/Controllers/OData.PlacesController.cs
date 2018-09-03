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

namespace PingDong.Newmoon.Events.Controllers.OData
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    public class PlacesODataController : BaseODataController
    {
        private readonly IPlaceQuery _placeQuery;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mediator"></param>
        /// <param name="placeQuery"></param>
        public PlacesODataController(ILogger<PlacesODataController> logger, IMediator mediator, IPlaceQuery placeQuery)
            : base(logger, mediator)
        {
            _placeQuery = placeQuery;
        }

        /// <summary>
        /// Get Places
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [ODataRoute("Places")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPlaces()
        {
            return Ok(await _placeQuery.GetAllAsync());
        }

        // POST api/v1/odata/Places
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

        // PUT api/v1/odata/Places
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