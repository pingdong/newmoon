using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc;
using PingDong.Newmoon.Events.Service.Queries.Models;
using PingDong.Newmoon.Events.Service.Queries.Rest;

namespace PingDong.Newmoon.Events.Controllers.Rest
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("api/v1/places/odata")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class PlacesODataController : BaseODataController<Place>
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="query"></param>
        public PlacesODataController(ILogger<EventsController> logger, IPlaceQuery query) 
            : base(logger, query)
        {
        }
    }
}