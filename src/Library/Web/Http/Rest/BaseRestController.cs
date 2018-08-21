using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingDong.DomainDriven.Service;

namespace PingDong.AspNetCore.Mvc.Rest
{
    /// <summary>
    /// Base Web API Controller
    /// </summary>
    [ApiController]
    public class BaseRestController : BaseController
    {
        private readonly IMediator _mediator;

        #region ctor

        /// <summary>
        /// ctor
       /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mediator"></param>
        public BaseRestController(ILogger logger, IMediator mediator)
            : base(logger)
        {
            _mediator = mediator;
        }

        #endregion

        #region Command

        [NonAction]
        protected async Task<IActionResult> CommandDispatchAsync<TCommand, TResponse>(TCommand command) where TCommand : IRequest<TResponse>
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);

            return Ok(result);
        }

        [NonAction]
        protected async Task<IActionResult> CommandDispatchAsync<TCommand>(string requestIdInString, TCommand command) where TCommand: IRequest<bool>
        {
            var commandResult = false;
            if (Guid.TryParse(requestIdInString, out var requestId) && requestId != Guid.Empty)
            {
                var identifiedCommand = new IdentifiedCommand<TCommand, bool>(requestId, command);
                commandResult = await _mediator.Send(identifiedCommand).ConfigureAwait(false);
            }

            return commandResult ? Ok() : BadRequest();
        }

        #endregion

        #region Query

        [NonAction]
        protected async Task<IActionResult> GetAllAsync<T>(IQuery<T> query)
        {
            var result = await query.GetAllAsync().ConfigureAwait(false);

            return Success(result);
        }

        [NonAction]
        protected async Task<IActionResult> GetByIdAsync<T>(ISingleQuery<T> query, int id)
        {
            var result = await query.GetByIdAsync(id).ConfigureAwait(false);

            return Success(result);
        }

        #endregion
    }
}
