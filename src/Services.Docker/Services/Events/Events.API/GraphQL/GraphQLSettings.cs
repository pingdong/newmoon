using System;
using Microsoft.AspNetCore.Http;

namespace PingDong.Newmoon.Events.GraphQL
{
    /// <summary>
    /// GraphQL Settings
    /// </summary>
    public class GraphQLSettings
    {
        /// <summary>
        /// Path
        /// </summary>
        public PathString Path { get; set; } = "/api/graphql";
        /// <summary>
        /// Func used to build user context
        /// </summary>
        public Func<HttpContext, object> BuildUserContext { get; set; }
    }
}
