using System.Net;

namespace SudLife_MasterField.APILayer.API.Global.FException
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionMessageAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = statusCode;

            var result = JsonConvert.SerializeObject(new
            {
                statusCode = statusCode,
                ErrorMessage = ex.Message
            });

            await context.Response.WriteAsync(result);
        }
    }
}
