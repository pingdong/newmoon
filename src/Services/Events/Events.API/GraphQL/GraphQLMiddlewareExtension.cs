using Microsoft.AspNetCore.Builder;

namespace PingDong.Newmoon.Events.GraphQL
{
    internal static class GraphQLMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphQL(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GraphQLMiddleware>(
                            new GraphQLSettings {
                                    BuildUserContext = ctx => new GraphQLUserContext { User = ctx.User }
                                }
                         );
        }
    }
}
