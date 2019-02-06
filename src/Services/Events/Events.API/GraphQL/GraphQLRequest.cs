using Newtonsoft.Json.Linq;

namespace PingDong.Newmoon.Events.GraphQL
{
    /// <summary>
    /// GraphQL Request
    /// </summary>
    public class GraphQLRequest
    {
        /// <summary>
        /// Operation name
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// Query
        /// </summary>
        public string Query { get; set; }
        /// <summary>
        /// Variables
        /// </summary>
        public JObject Variables { get; set; }
    }
}
