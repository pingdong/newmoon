using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.TestHost;

namespace PingDong.QualityTools.AspNetCore
{
    public static class HttpClientExtensions
    {
        private const string RequestIdHeader = "x-requestid";

        public static HttpClient CreateDefaultClient(this TestServer server)
        {
            var client = server.CreateClient();
            client.DefaultRequestHeaders.Add(RequestIdHeader, Guid.NewGuid().ToString());
            return client;
        }


        public static HttpClient Reset(this HttpClient http)
        {
            return http.RefreshRequestId();
        }

        public static HttpClient RefreshRequestId(this HttpClient http)
        {
            if (http.DefaultRequestHeaders.Contains(RequestIdHeader))
                http.DefaultRequestHeaders.Remove(RequestIdHeader);

            http.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());

            return http;
        }

        public static StringContent CreateJsonContent(this string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
