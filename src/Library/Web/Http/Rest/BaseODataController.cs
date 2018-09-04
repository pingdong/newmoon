using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.DomainDriven.Service;

namespace PingDong.AspNetCore.Mvc.OData
{
    /// <summary>
    /// Base Web API Controller
    /// </summary>
    public class BaseODataController : ODataController
    {
        private readonly IMediator _mediator;

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mediator"></param>
        public BaseODataController(ILogger logger, IMediator mediator)
        {
            _mediator = mediator;
            Logger = logger;
        }

        #endregion

        #region Logging
        /// <summary>
        /// Logger
        /// </summary>
        protected ILogger Logger { get; }

        protected string LoggingPrefix { get; }
        #endregion

        #region Command

        [NonAction]
        protected async Task<IActionResult> CommandDispatchAsync<TCommand, TResponse>(TCommand command) where TCommand : IRequest<TResponse>
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);

            return Ok(result);
        }

        [NonAction]
        protected async Task<IActionResult> CommandDispatchAsync<TCommand>(string requestIdInString, TCommand command) where TCommand : IRequest<bool>
        {
            var commandResult = false;
            if (Guid.TryParse(requestIdInString, out var requestId) && requestId != Guid.Empty)
            {
                var identifiedCommand = new IdentifiedCommand<TCommand, bool>(requestId, command);
                commandResult = await _mediator.Send(identifiedCommand).ConfigureAwait(false);
            }

            return commandResult ? (IActionResult) Ok() : BadRequest();
        }

        #endregion
    }
}

