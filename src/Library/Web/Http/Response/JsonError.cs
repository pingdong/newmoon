using System.Collections.Generic;

namespace PingDong.AspNetCore.Http
{
    /// <summary>
    /// Error
    /// </summary>
    public class JsonError
    {
        /// <summary>
        /// Error Code
        /// 
        /// The value for the "code" name/value pair is a language-independent string.
        /// Its value is a service-defined error code that should be human-readable.
        /// The code serves as more specific indicator of the error than the HTTP error code specified in the response.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Error Message
        /// 
        /// The value for the "message" name/value pair must be human-readable representation of the error.
        /// It is intended as an aid to developers and is not suitable for exposure to end users.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Target
        /// 
        /// The value for the "target" name/value pair is the target of the particular error. (e.g., the name of the property in the error).
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// Target
        /// 
        /// The value for the "requestValue" name/value pair is the request value of the particular error. (e.g., the value of the property in the error).
        /// </summary>
        public object RequestValue { get; set; }
        /// <summary>
        /// Error details
        /// 
        /// The value for the "details" name/value pair must be an array of JSON object that must contains name/value pairs for "code" and "message",
        /// and may contain a name/value pair for "target", as described above.
        /// </summary>
        public List<JsonError> Details { get; set; }
    }
}
