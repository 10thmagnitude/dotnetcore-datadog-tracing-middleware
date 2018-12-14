using System.Threading.Tasks;
using Datadog.Trace;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DotNet.Tracing.Middleware
{
    public class DataDogTracing
    {
        private readonly RequestDelegate _next;
        private readonly string _serviceName;

        public DataDogTracing(RequestDelegate next, string serviceName)
        {
            _next = next;
            _serviceName = serviceName;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = Tracer.Instance.StartActive("aspnet_core_mvc.request", serviceName:_serviceName))
            {
                var span = scope.Span;
                span.Type = SpanTypes.Web;
                span.ResourceName = $"{context.Request.Method} {context.Request.Path}";
                span.SetTag("sourcecategory", "sourcecode");
                span.SetTag("source", "csharp");

                await _next(context);
            }
        }
    }

    public static class DataDogTracingExtensions
    {
        public static IApplicationBuilder UseDataDogTracing(this IApplicationBuilder builder, string serviceName)
        {
            return builder.UseMiddleware<DataDogTracing>(serviceName);
        }
    }
}
