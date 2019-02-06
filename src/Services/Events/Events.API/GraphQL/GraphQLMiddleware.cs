using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PingDong.Newmoon.Events.GraphQL
{
    internal class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GraphQLSettings _settings;
        private readonly IDocumentExecuter _executor;
        private readonly IDocumentWriter _writer;
        private readonly ISchema _schema;

        public GraphQLMiddleware(
            RequestDelegate next,
            GraphQLSettings settings,
            IDocumentExecuter executor, 
            IDocumentWriter writer,
            ISchema schema)
        {
            _next = next;

            _settings = settings;

            _executor = executor;
            _writer = writer;
            _schema = schema;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!IsGraphQLRequest(context))
            {
                await _next(context);

                return;
            }

            var result = await ExecuteAsync(context, _schema);

            CheckForErrors(result);

            await WriteResponseAsync(context, result);
        }

        #region Private Methods
        private bool IsGraphQLRequest(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments(_settings.Path)
                   && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase);
        }

        private async Task<ExecutionResult> ExecuteAsync(HttpContext context, ISchema schema)
        {
            var request = Deserialize<GraphQLRequest>(context.Request.Body);

            return await _executor.ExecuteAsync(option =>
                {
                    option.Schema = schema;
                    option.Query = request.Query;
                    option.OperationName = request.OperationName;
                    option.Inputs = request.Variables.ToInputs();
                    option.UserContext = _settings.BuildUserContext?.Invoke(context);
                    option.ValidationRules = DocumentValidator.CoreRules().Concat(new[] { new InputValidationRule() });
                });
        }

        private async Task WriteResponseAsync(HttpContext context, ExecutionResult result)
        {
            var response = await _writer.WriteToStringAsync(result);

            context.Response.StatusCode = result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(response);
        }

        private static T Deserialize<T>(Stream s)
        {
            using (var reader = new StreamReader(s))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var ser = new JsonSerializer();

                return ser.Deserialize<T>(jsonReader);
            }
        }

        private void CheckForErrors(ExecutionResult result)
        {
            if (!(result.Errors?.Count > 0))
                return;

            var errors = new List<Exception>();
            foreach (var error in result.Errors)
            {
                var ex = new Exception(error.Message);

                if (error.InnerException != null)
                {
                    ex = new Exception(error.Message, error.InnerException);
                }
                errors.Add(ex);
            }

            throw new AggregateException(errors);
        }
        #endregion
    }
}
