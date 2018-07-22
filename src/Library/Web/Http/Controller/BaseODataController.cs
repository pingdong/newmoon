using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.DomainDriven.Service;

namespace PingDong.AspNetCore.Mvc
{
    /// <summary>
    /// Base Web API Controller
    /// </summary>
    [ApiController]
    public class BaseODataController<TSummary> : ODataController
    {
        #region

        private readonly IQuery<TSummary> _query;

        #endregion

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="query"></param>
        public BaseODataController(ILogger logger, IQuery<TSummary> query)
        {
            Logger = logger;

            _query = query;
        }

        #endregion

        #region Query
        [EnableQuery]
        public async Task<IQueryable<TSummary>> GetAsync()
        {
            var result = await _query.GetAllAsync();

            return result.AsQueryable();
        }

        #endregion

        #region Logging
        /// <summary>
        /// Logger
        /// </summary>
        protected ILogger Logger { get; }

        protected string LoggingPrefix { get; }
        #endregion
    }
}

