using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PingDong.Web.Http.GraphQL;

namespace PingDong.Newmoon.Events.Middlewares
{
    internal class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GraphQLMiddleware> _logger;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLMiddleware(RequestDelegate next, ILogger<GraphQLMiddleware> logger,
                                IDocumentExecuter documentExecuter, ISchema schema)
        {
            _next = next;

            _logger = logger;

            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api/graphql"))
            {
                using (var sr = new StreamReader(httpContext.Request.Body))
                {
                    var body = await sr.ReadToEndAsync();

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        var request = JsonConvert.DeserializeObject<QueryRequest>(body);

                        var executionOptions = new ExecutionOptions
                        {
                            Schema = _schema,
                            Query = request.Query
                        };

                        var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

                        CheckForErrors(result);

                        await WriteResult(httpContext, result);
                        
                        return;
                    }
                }
            }

            await _next(httpContext);
        }

        private async Task WriteResult(HttpContext httpContext, ExecutionResult result)
        {
            var json = new DocumentWriter(indent: true).Write(result);
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
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
    }
}
