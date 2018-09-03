using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Controllers.OData
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    public class PlacesController : ODataController
    {
        private readonly ILogger<PlacesController> _logger;
        private readonly IPlaceQuery _placeQuery;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="placeQuery"></param>
        public PlacesController(ILogger<PlacesController> logger, IPlaceQuery placeQuery)
        {
            _logger = logger;
            _placeQuery = placeQuery;
        }

        /// <summary>
        /// Get Places
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [ODataRoute("Places")]
        public async Task<IActionResult> GetPlaces()
        {
            return Ok(await _placeQuery.GetAllAsync());
        }
    }
}