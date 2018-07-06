using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Service.Queries;
using PingDong.Web.Http;

namespace PingDong.Newmoon.Events.Controllers
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("api/v1/places/odata")]
    [Produces("application/json")]
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