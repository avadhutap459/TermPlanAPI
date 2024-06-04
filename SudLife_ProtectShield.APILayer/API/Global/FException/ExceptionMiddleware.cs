using Microsoft.AspNetCore.Http.Extensions;
using NLog;
using SudLife_ProtectShield.APILayer.API.Model;
using SudLife_ProtectShield.APILayer.API.Service.Common;

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

            List<string> Msgstr = new List<string>();
            Msgstr.Add("Internal Server Error");

            BaseResponse baseResponse = new BaseResponse()
            {
                StatusCode = 500,
                Message = Msgstr,
                IsSuccess = false,
                Data = null,
            };



            string ExcRes = _JsonConvert.SerializeObject(baseResponse);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            context.Response.WriteAsync(ExcRes.ToString());

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
