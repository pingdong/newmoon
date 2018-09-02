using Microsoft.AspNetCore.Builder;

namespace PingDong.Newmoon.Events.Middlewares.GraphQL
{
    internal static class GraphQlMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphQL(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GraphQLMiddleware>();
        }
    }
}
