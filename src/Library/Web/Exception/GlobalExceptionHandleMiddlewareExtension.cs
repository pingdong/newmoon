using Microsoft.AspNetCore.Builder;

namespace PingDong.AspNetCore.Http
{
    /// <summary>
    /// Extension for adding exception handling middleware
    /// </summary>
    public static class GlobalExceptionHandleMiddlewareExtension
    {
        /// <summary>
        /// Adding to pipeline
        /// </summary>
        /// <param name="builder">Application</param>
        /// <returns>Application</returns>
        public static IApplicationBuilder UseGlobalExceptionHandle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandleMiddleware>();
        }
    }
}