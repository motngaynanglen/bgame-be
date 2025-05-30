﻿using Microsoft.AspNetCore.Http;

namespace BG_IMPACT.Infrastructure.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetName(this HttpContext context)
        {
            return context.User?.Claims?.SingleOrDefault(p => p.Type.Contains("name"))?.Value ?? string.Empty;
        }

        public static string GetRole(this HttpContext context)
        {
            return context.User?.Claims?.SingleOrDefault(p => p.Type.Contains("role"))?.Value ?? string.Empty;
        }
    }
}
