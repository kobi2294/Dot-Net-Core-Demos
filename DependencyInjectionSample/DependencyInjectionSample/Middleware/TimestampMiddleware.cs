using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Middleware
{
    public class TimestampMiddleware
    {
        private readonly RequestDelegate _next;

        public TimestampMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var now = DateTime.Now;
            context.Items.Add("Now", now);
            Console.WriteLine("It is now: " + now);

            await _next.Invoke(context);
        }
    }

    public static class TimestampMiddlewareExtension
    {
        public static IApplicationBuilder UseTimestampMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimestampMiddleware>();
        }
    }
}
