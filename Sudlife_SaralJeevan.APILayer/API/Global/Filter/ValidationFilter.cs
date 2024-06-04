using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Newtonsoft.Json.Linq;

namespace Sudlife_SaralJeevan.APILayer.API.Global.Filter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ILogger<ValidationFilter> _logger;
        public ValidationFilter( ILogger<ValidationFilter> logger) ///, ICommonSvc ICommonSvc
        {
          
            _logger = logger;
          
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {


                await next();

            }
            else
            {
                BaseResponse baseResponse = new BaseResponse();
                baseResponse.StatusCode = 400;
                var ModelState = context.ModelState;

                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
               
                baseResponse.Message = errors;
                baseResponse.IsSuccess = false;
                baseResponse.Data =null;

              
                context.Result = new ContentResult
                {
                    Content = Convert.ToString(_JsonConvert.SerializeObject(baseResponse)),
                    ContentType = "application/json",
                    StatusCode = 400
                };
            }
        }
    }
}
