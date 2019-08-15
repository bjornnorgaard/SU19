using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace PLT.Middleware
{
    public class ChillMiddleware
    {
        private readonly RequestDelegate _next;

        public ChillMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            Thread.Sleep(2000);
        }
    }

    public static class SlowMiddlewareExtension
    {
        public static IApplicationBuilder UseChillMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ChillMiddleware>();
            return builder;
        }
    }
}
