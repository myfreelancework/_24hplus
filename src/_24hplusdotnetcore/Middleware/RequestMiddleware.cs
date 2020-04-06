using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Builder;

namespace _24hplusdotnetcore.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers["Authorization"].Count > 0)
            {
                var auth = context.Request.Headers["Authorization"][0];
            }
            else
            {
                if (context.Request.Path.Value.Contains("api/auth/userlogin") || context.Request.Path.Value.Contains("swagger"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.Headers.Clear();
                }
            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
    public static class RequestAPIMiddlewareExtensions
    {
        public static IApplicationBuilder RequestAPIMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }
    }
}