using Microsoft.AspNetCore.Http.Extensions;
using NLog;
using SudLife_ProtectShield.APILayer.API.Model;

namespace SudLife_ProtectShield.APILayer.API.Global.FException
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

                logger.Info("Entered ExceptionMiddleware");
                logger.Info("Exception ex: " + ex.ToString());
                var url1 = context.Request.GetDisplayUrl();

                String[] strlist = url1.Split("/");
                string ControllerName = strlist[3];
                string ActionMethodName = strlist[4];



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

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            context.Response.WriteAsync(errorResponse.ToString());

        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static void CongigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

        }
    }
}
