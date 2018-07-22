﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using PingDong.Application.Logging;
using PingDong.AspNetCore.Http;
using PingDong.DomainDriven.Service;

namespace PingDong.AspNetCore.Mvc
{
    /// <summary>
    /// Base Web API Controller
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor

        /// <summary>
        /// ctor
       /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mediator"></param>
        public BaseController(ILogger logger, IMediator mediator)
        {
            _mediator = mediator;

            Logger = logger;
        }

        #endregion

        #region Command

        [NonAction]
        protected async Task<IActionResult> CommandDispatchAsync<TCommand, TResponse>(TCommand command) where TCommand : IRequest<TResponse>
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [NonAction]
        protected async Task<IActionResult> CommandDispatchAsync<TCommand>(string requestIdInString, TCommand command) where TCommand: IRequest<bool>
        {
            var commandResult = false;
            if (Guid.TryParse(requestIdInString, out var requestId) && requestId != Guid.Empty)
            {
                var identifiedCommand = new IdentifiedCommand<TCommand, bool>(requestId, command);
                commandResult = await _mediator.Send(identifiedCommand);
            }

            return commandResult ? Ok() : BadRequest();
        }

        #endregion

        #region Query

        [NonAction]
        protected async Task<IActionResult> GetAllAsync<T>(IQuery<T> query)
        {
            var result = await query.GetAllAsync();

            return Success(result);
        }

        [NonAction]
        protected async Task<IActionResult> GetByIdAsync<T>(ISingleQuery<T> query, int id)
        {
            var result = await query.GetByIdAsync(id);

            return Success(result);
        }

        #endregion

        #region Logging
        /// <summary>
        /// Logger
        /// </summary>
        protected ILogger Logger { get; }

        protected string LoggingPrefix { get; }
        #endregion

        #region Response

        #region Success

        /// <summary>
        /// Success
        /// </summary>
        /// <param name="value">Queryable of data</param>
        /// <param name="caller"></param>
        /// <returns>Object Result</returns>
        [NonAction]
        protected IActionResult Success<T>(IEnumerable<T> value, [CallerMemberName] string caller = null)
        {
            var response = new JsonResponse
                {
                    Success = true,
                    Value = value
                };

            Logger.LogInformation(LoggingEvent.Success, $"{LoggingPrefix} '{caller}' Success");

            return Ok(response);
        }

        /// <summary>
        /// Success on retrieving single object
        /// </summary>
        /// <param name="value">object</param>
        /// <param name="caller"></param>
        /// <returns>Object Result</returns>
        [NonAction]
        protected IActionResult Success<T>(T value, [CallerMemberName] string caller = null)
        {
            var response = new JsonResponse
                {
                    Success = true,
                    Value = value
                };

            Logger.LogInformation(LoggingEvent.Success, $"{LoggingPrefix} '{caller}' Success");

            return Ok(response);
        }

        /// <summary>
        /// Success
        /// </summary>
        /// <returns>Object Result</returns>
        [NonAction]
        protected IActionResult Success([CallerMemberName] string caller = null)
        {
            var response = new JsonResponse
                {
                    Success = true
                };

            Logger.LogInformation(LoggingEvent.Success, $"{LoggingPrefix} '{caller}' Success");

            return Ok(response);
        }

        /// <summary>
        /// Success and then navigate
        /// </summary>
        /// <param name="id">Object's Id</param>
        /// <param name="value">Object</param>
        /// <param name="caller"></param>
        /// <returns>Redirect to new created object</returns>
        [NonAction]
        protected CreatedAtRouteResult Created<T>(int id, T value, [CallerMemberName] string caller = null)
        {
            var response = new JsonResponse
                {
                    Success = true,
                    Value = value
            };

            Logger.LogInformation(LoggingEvent.Success, $"{LoggingPrefix} '{caller}' Success: '{id}' created");

            return CreatedAtRoute("Get", routeValues: new { id = id }, value: response);
        }

        #endregion

        #region Error

        /// <summary>
        /// Can't find specified object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="caller"></param>
        /// <returns>NotFound</returns>
        [NonAction]
        protected IActionResult ObjectNotFound(int id, [CallerMemberName] string caller = null)
        {
            var error = new JsonErrorResponse
            {
                Success = false,
                Error = new JsonError { Code = StatusCodes.Status404NotFound, Message = $"Object:{id} Not found" }
            };

            Logger.LogWarning(LoggingEvent.ObjectNotFound, $"{LoggingPrefix} '{caller}' failed: '{id}' not found");

            return NotFound(error);
        }

        /// <summary>
        /// Invalid request
        /// </summary>
        /// <param name="modelState">Model State</param>
        /// <param name="caller"></param>
        /// <returns>Bad Request</returns>
        [NonAction]
        protected IActionResult BadRequest(ModelStateDictionary modelState = null, [CallerMemberName] string caller = null)
        {
            var error = new JsonErrorResponse
            {
                Success = false,
                Error = new JsonError { Code = StatusCodes.Status400BadRequest, Message = "Invalid Request" }
            };

            error.Error.RequestValue = modelState;
            
            Logger.LogWarning(LoggingEvent.ReceiveInvalidData, $"{LoggingPrefix} '{caller}' failed: Invalid ModelState", modelState);

            return BadRequest(error);
        }
        /// <summary>
        /// Failed to verify data
        /// </summary>
        /// <param name="exception">VerificationException</param>
        /// <param name="caller"></param>
        /// <returns>BadRequest</returns>
        [NonAction]
        protected IActionResult VerficationFailed(VerificationException exception, [CallerMemberName] string caller = null)
        {
            var error = new JsonErrorResponse
            {
                Success = false,
                Error = new JsonError { Code = StatusCodes.Status400BadRequest, Message = exception.Message }
            };

            Logger.LogWarning(LoggingEvent.ReceiveInvalidData, $"{LoggingPrefix} '{caller}' failed on verification: {exception.Message}");

            return BadRequest(error);
        }
        /// <summary>
        /// Object is null or empty
        /// </summary>
        /// <param name="caller"></param>
        /// <returns>BadRequest</returns>
        [NonAction]
        protected IActionResult InvalidObjectRequest([CallerMemberName] string caller = null)
        {
            var error = new JsonErrorResponse
            {
                Success = false,
                Error = new JsonError { Code = StatusCodes.Status400BadRequest, Message = "Invalid Object" }
            };

            Logger.LogWarning(LoggingEvent.ReceiveInvalidData, $"{LoggingPrefix} '{caller}' failed: Invalid Object");

            return BadRequest(error);
        }
        /// <summary>
        /// Object is already existed
        /// </summary>
        /// <param name="id">Object's Id</param>
        /// <param name="caller"></param>
        /// <returns>Object Result</returns>
        [NonAction]
        protected IActionResult ObjectExisted(int id, [CallerMemberName] string caller = null)
        {
            var error = new JsonErrorResponse
            {
                Success = false,
                Error = new JsonError { Code = 409, Message = $"Can't create duplicated object:{id}" }
            };

            Logger.LogWarning(LoggingEvent.CreateDuplicatedData, $"{LoggingPrefix} '{caller}' failed: {error.Error.Message}");

            return StatusCode(StatusCodes.Status409Conflict);
        }


        #endregion

        #endregion
    }
}
