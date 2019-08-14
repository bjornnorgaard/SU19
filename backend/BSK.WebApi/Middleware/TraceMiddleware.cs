using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BSK.WebApi.Middleware
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var guid = Guid.NewGuid();
            context.Items.Add("TraceId", guid);
            await _next(context);
        }
    }
}
