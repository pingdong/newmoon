using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Events.Service.Queries;

namespace PingDong.Newmoon.Events.Controllers.Rest
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    public class EventsODataController : ODataController
    {
        private readonly ILogger<EventsODataController> _logger;
        private readonly IEventQuery _eventQuery;
        private readonly IPlaceQuery _placeQuery;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="eventQuery"></param>
        /// <param name="placeQuery"></param>
        public EventsODataController(ILogger<EventsODataController> logger, IEventQuery eventQuery, IPlaceQuery placeQuery)
        {
            _logger = logger;
            _eventQuery = eventQuery;
            _placeQuery = placeQuery;
        }

        /// <summary>
        /// Get Events
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [ODataRoute("Events")]
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
        [ODataRoute("Events({id})")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var evt = await _eventQuery.GetByIdAsync(id);

            return evt == null ? (IActionResult) NotFound() : Ok(evt);
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