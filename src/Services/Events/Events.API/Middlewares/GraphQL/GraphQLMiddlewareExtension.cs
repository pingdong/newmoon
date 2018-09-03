using Microsoft.AspNetCore.Builder;

namespace PingDong.Newmoon.Events.Middlewares
{
    internal static class GraphQLMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphQL(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GraphQLMiddleware>();
        }
    }
}
