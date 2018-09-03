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
    public class EventsController : ODataController
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventQuery _eventQuery;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="eventQuery"></param>
        public EventsController(ILogger<EventsController> logger, IEventQuery eventQuery)
        {
            _logger = logger;
            _eventQuery = eventQuery;
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
    }
}