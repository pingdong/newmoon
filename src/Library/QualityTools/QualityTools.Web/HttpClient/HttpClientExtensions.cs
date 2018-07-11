using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.TestHost;

namespace PingDong.QualityTools.Web
{
    public static class HttpClientExtensions
    {
        private const string RequestIdHeader = "x-requestid";

        public static HttpClient CreateIdempotentClient(this TestServer server)
        {
            var client = server.CreateClient();
            client.DefaultRequestHeaders.Add(RequestIdHeader, Guid.NewGuid().ToString());
            return client;
        }
        public static void RefreshRequestId(this HttpClient http)
        {
            if (http.DefaultRequestHeaders.Contains(RequestIdHeader))
                http.DefaultRequestHeaders.Remove(RequestIdHeader);

            http.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());
        }

        public static StringContent CreateJsonContent(this string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
