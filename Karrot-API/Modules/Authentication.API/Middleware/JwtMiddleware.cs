using Authentication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Authentication.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IAuthenticationService authService)
        {
            var authorizeAttribute = context.GetEndpoint()?.Metadata.GetMetadata<AuthorizeAttribute>();
            string? authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizeAttribute != null)
            {
                if (authHeader != null && authHeader.StartsWith("Bearer "))
                {
                    string token = authHeader.Substring("Bearer ".Length).Trim();

                    ClaimsPrincipal? principal = await authService.ValidateTokenAsync(token);


                    if (principal != null)
                    {
                        context.User = principal;
                    }
                    else
                    {
                        context.Response.StatusCode = 498;
                        context.Response.ContentType = "application/json";

                        // Customize the response message
                        var errorMessage = "Error: Invalid token";
                        var errorBytes = Encoding.UTF8.GetBytes(errorMessage);
                        await context.Response.Body.WriteAsync(errorBytes, 0, errorBytes.Length);
                        return;
                    }  
                }
                else
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            await _next(context);
        }
    }
}
