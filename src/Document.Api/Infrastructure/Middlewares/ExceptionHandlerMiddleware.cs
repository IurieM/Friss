using Document.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Document.Api.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        static readonly ILogger Log = Serilog.Log.ForContext<ExceptionHandlerMiddleware>();

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (AppException ex)
            {
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = (int)ex.StatusCode;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal_Server_Error");
            }
        }
    }
}
