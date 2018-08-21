using System;
using System.Net;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.AspNetCore.Mvc;
using PingDong.Web.Http.GraphQL;

namespace PingDong.Newmoon.Events.Controllers.Rest
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("api/graphql")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class GraphQLController : BaseController
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="logger">logger</param>
        /// <param name="documentExecuter"></param>
        public GraphQLController(IDocumentExecuter documentExecuter, ISchema schema, ILogger<GraphQLController> logger)
            : base(logger)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        // POST api/Places
        /// <summary>
        /// Create a new place
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromHeader(Name = "x-requestid")] string requestId, [FromBody]QueryRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request)); 

            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = request.Query
            };

            try
            {
                var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

                if (result.Errors?.Count > 0)
                    return BadRequest(result);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}