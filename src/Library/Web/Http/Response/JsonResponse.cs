namespace PingDong.AspNetCore.Http
{
    /// <summary>
    /// Response
    /// </summary>
    public class JsonResponse : JsonResponseBase
    {
        /// <summary>
        /// Pagination
        /// </summary>
        public JsonPagination Pagination { get; set; }
        /// <summary>
        /// Payload
        /// </summary>
        public object Value { get; set; }
    }
}
