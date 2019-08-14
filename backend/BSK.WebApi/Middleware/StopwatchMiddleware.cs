using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BSK.WebApi.Middleware
{
    public class StopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public StopwatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"Request started {context.Items["TraceId"]}");
            await _next(context);
            Console.WriteLine($"Request finished in {sw.ElapsedMilliseconds}ms");
        }
    }
}