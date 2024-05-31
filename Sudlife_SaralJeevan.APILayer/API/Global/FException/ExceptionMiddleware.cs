using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sudlife_SaralJeevan.APILayer.API.Database;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using NLog;

namespace Sudlife_SaralJeevan.APILayer.API.Global.FException
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {

                await next(context);
            }
            catch (Exception ex)
            {

                var url1 = context.Request.GetDisplayUrl();

                await HandleException(context, ex);

            }
        }

        public async Task HandleException(HttpContext context, Exception ex)
        {

            int statusCode = StatusCodes.Status500InternalServerError;
            switch (ex)
            {
                case FileNotFoundException _:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
                case DivideByZeroException _:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
            }

            var errorResponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = ex.Message,
            };




            string DecResponse = _JsonConvert.SerializeObject(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            context.Response.WriteAsync(errorResponse.ToString());

        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

        }
    }
}
