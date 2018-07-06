using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PingDong.Newmoon.Events.Controllers
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class PingController : Controller
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="logger">logger</param>
        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        // GET api/ping
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            _logger.LogInformation("Ping is called");

            return new JsonResult(from c
                                    in User.Claims
                                  select new { c.Type, c.Value });
        }
    }
}