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
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder RequestAPIMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }
    }
}