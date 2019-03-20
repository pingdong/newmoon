using System.Security.Claims;

namespace PingDong.Newmoon.Events.GraphQL
{
    /// <summary>
    /// User context
    /// </summary>
    public class GraphQLUserContext
    {
        /// <summary>
        /// User
        /// </summary>
        public ClaimsPrincipal User { get; set; }
    }
}
