namespace PingDong.AspNetCore.Http
{
    /// <summary>
    /// Response
    /// </summary>
    public class JsonErrorResponse : JsonResponseBase
    {
        /// <summary>
        /// Error
        /// </summary>
        public JsonError Error { get; set; }
    }
}
