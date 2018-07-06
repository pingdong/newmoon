using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using PingDong.Application.Logging;
using PingDong.Data;
using PingDong.Validation;
using PingDong.Web.Http;
using Polly.CircuitBreaker;

namespace PingDong.Web.Exceptions
{
    /// <summary>
    /// Middleware for handling exception
    /// </summary>
    public class GlobalExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandleMiddleware> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next">Next middleware in the pipeline</param>
        /// <param name="logger">logger</param>
        public GlobalExceptionHandleMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context">Http Context</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvent.UnhandleException, ex, "Unhandled expection");

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning(LoggingEvent.UnhandleException, $"[{context.Session?.Id}]: The response has already started, the http status code middleware will not be executed.");

                    throw;
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var innerError = new JsonError { Message = exception.Message };

            switch (exception)
            {
                case ArgumentOutOfRangeException aore:
                    innerError.Code = StatusCodes.Status404NotFound;
                    innerError.Target = aore.ParamName;
                    innerError.RequestValue = aore.ActualValue;
                    break;
                case ArgumentNullException ane:
                    innerError.Code = StatusCodes.Status400BadRequest;
                    innerError.Target = ane.ParamName;
                    break;
                case ObjectExistedException ade:
                    innerError.Code = StatusCodes.Status409Conflict;
                    innerError.Target = ade.TypeName;
                    innerError.RequestValue = ade.DuplicatedValue;
                    break;
                case UnauthorizedAccessException uae:
                    innerError.Code = StatusCodes.Status401Unauthorized;
                    break;
                case BrokenCircuitException bce:
                    innerError.Code = StatusCodes.Status500InternalServerError;
                    break;
                case DbUpdateConcurrencyException db:
                    innerError.Code = StatusCodes.Status409Conflict;

                    foreach (var entry in db.Entries)
                    {
                        var detail = new JsonError
                        {
                            Code = StatusCodes.Status409Conflict,
                            Message = "Data is modified after loading",
                            Target = entry.Metadata.Name
                        };

                        foreach (var property in entry.Metadata.GetProperties())
                        {
                            var detailError = new JsonError
                            {
                                Code = StatusCodes.Status410Gone,
                                Target = property.Name,
                                RequestValue = entry.Property(property.Name).CurrentValue
                            };
                            detail.Details.Add(detailError);
                        }

                        innerError.Details.Add(detail);
                    }
                    break;
                case ValidationException ve:
                    innerError.Code = StatusCodes.Status406NotAcceptable;

                    foreach (var detail in ve.Details)
                    {
                        var detailError = new JsonError
                        {
                            Code = StatusCodes.Status406NotAcceptable,
                            Message = detail.ErrorMessage,
                            RequestValue = detail.AttemptedValue
                        };

                        innerError.Details.Add(detailError);
                    }
                    break;
                case ArgumentException ae:
                    innerError.Code = StatusCodes.Status400BadRequest;
                    innerError.Target = ae.ParamName;
                    break;
                default:
                    innerError.Code = StatusCodes.Status500InternalServerError;
                    break;
            }

            var error = new JsonErrorResponse
                {
                    Success = false,
                    Error = innerError
                };
            
            _logger.LogWarning(LoggingEvent.UnhandleException, $"[{context.Session?.Id}]: Unhandled exception", exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = innerError.Code;
            context.Response.Headers.Clear();
            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}