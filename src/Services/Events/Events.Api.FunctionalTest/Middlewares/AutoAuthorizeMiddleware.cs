﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Events.Functional.Test.Infrastructure
{
    public class AutoAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        public AutoAuthorizeMiddleware(RequestDelegate rd)
        {
            _next = rd;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var identity = new ClaimsIdentity("AutoAuth");
            identity.AddClaim(new Claim("sub", "9e3163b9-1ae6-4652-9dc6-7898ab7b7a00"));
            identity.AddClaim(new Claim("scope", "events.api"));

            httpContext.User.AddIdentity(identity);

            await _next.Invoke(httpContext);
        }
    }
}
