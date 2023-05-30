using Microsoft.AspNetCore.Authentication;
using System.Data;
using System.Security.Claims;

namespace IdentitySPA.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string? url = context.Request.Headers["Referer"];
            if (url != null)
            {
                var uri = new Uri(url);
                context.Items["Url"] = uri.Host + (uri.IsDefaultPort ? "" : ":" + uri.Port);
            }

            await _next(context);
        }
    }
}
