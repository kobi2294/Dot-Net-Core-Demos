using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Middleware
{
    public class MetadataMiddleware
    {
        private RequestDelegate _next;
        private IPeopleRepositoryService _peopleService;

        public MetadataMiddleware(RequestDelegate next, IPeopleRepositoryService peopleService)
        {
            _next = next;
            _peopleService = peopleService;
        }

        public async Task Invoke(HttpContext context, IPeopleRepositoryService peopleService)
        {
            var header = context.Request.Headers;
            var accept = header["Accept"].ToString();
            var userAgent = header["User-Agent"].ToString();
            var host = header["Host"].ToString();
            var peopleCount = (await _peopleService.GetAllPeople()).Length;
            var peopleCount2 = (await _peopleService.GetAllPeople()).Length;
        }
    }

    public static class MetadataMiddlewareExtensions
    {
        public static IApplicationBuilder UserMetadataMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MetadataMiddleware>();
        }
    }
}
