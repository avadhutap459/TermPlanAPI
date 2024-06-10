namespace SudLife_MasterField.APILayer.API.Global.FException
{
    public static class ExceptionHandlerMiddlewareExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
