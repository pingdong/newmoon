﻿using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PingDong.Newmoon.Events.Filters
{
    /// <summary>
    /// Document Filter
    /// </summary>
    public class SecurityRequirementsDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument document, DocumentFilterContext context)
        {
            document.Security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new string[]{ "openid", "profile", "email", "events.api" } },
                }
            };
        }
    }
}