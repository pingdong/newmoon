
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PingDong.Application.Logging;
using PingDong.Web.Http;

namespace PingDong.Web.Validation
{
    public class ModelStateValidationFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        public ModelStateValidationFilter() : this(null)
        {

        }
        public ModelStateValidationFilter(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var validationErrors = context.ModelState
                                          .Keys
                                          .SelectMany(k => context.ModelState[k].Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToString();

            var error = new JsonErrorResponse
                {
                    Success = false,
                    Error = new JsonError {Code = StatusCodes.Status400BadRequest, Message = validationErrors}
                };

            error.Error.RequestValue = context.ModelState;

            _logger?.LogWarning(LoggingEvent.ReceiveInvalidData,
                $"[{context.HttpContext.Session.Id}]: Invalid ModelState", context.ModelState);
            
            context.Result = new BadRequestObjectResult(error);
        }
    }
}
