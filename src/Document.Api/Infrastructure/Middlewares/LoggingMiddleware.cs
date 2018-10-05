using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Document.Api.Infrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        const string MessageTemplate =
            "Executed HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        static readonly ILogger Log = Serilog.Log.ForContext<LoggingMiddleware>();

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await next(context);
            sw.Stop();
            LogRequest(context, sw.Elapsed.TotalMilliseconds);
        }

        private void LogRequest(HttpContext context, double totalMilliseconds)
        {
            var statusCode = context.Response?.StatusCode;
            var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;
            Log.Write(level, MessageTemplate, context.Request.Method, context.Request.Path, statusCode, totalMilliseconds);
        }
    }
}
