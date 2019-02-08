using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc.OData;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Controllers.OData
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [ODataRoutePrefix("places")]
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
        [ODataRoute("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPlaces()
        {
            return Ok(await _placeQuery.GetAllAsync());
        }
    }
}