using Authentication.Application.Interfaces;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Security.Policy;

namespace Authentication.API.Middleware
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
            string? url = context.Request.Headers["KarrotUrl"];
            if (url != null)
            {
                var uri = new Uri(url);
                url = uri.Host + (uri.IsDefaultPort ? "" : ":" + uri.Port);

                context.Items["Url"] = url;
                if (!string.IsNullOrEmpty(url))
                {
                    Uri uriHost = new Uri(url);
                    string domain = uriHost.Host;

                    // Extract the domain (remove the subdomain)
                    string[] domainParts = domain.Split('.');
                    if (domainParts.Length > 1)
                    {
                        string extractedDomain = string.Join(".", domainParts.Skip(1));
                        context.Items["Domain"] = extractedDomain;
                    }
                }
            }
            else
                context.Items["Url"] = context.Request.Headers["Referer"];
            await _next(context);
        }
    }
}
