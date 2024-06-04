using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Sudlife_ProtectShieldPlus.APILayer.API.Model;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.Common;

namespace Sudlife_ProtectShieldPlus.APILayer.API.Global.Filter
{
    public class ValidationResultFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            BaseResponse response = new BaseResponse();
            

            try
            {
                if (context.ModelState.IsValid)
                {
                    await next();
                }
                else
                {
                    var errors = context.ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage)
                                    .ToList();

                    response.Message = errors;
                    response.StatusCode = 400;
                    response.IsSuccess = false;
                    response.Data = null;
                    context.Result = new ContentResult
                    {
                        Content = Convert.ToString(_JsonConvert.SerializeObject(response)),
                        ContentType = "application/json",
                        StatusCode = 400
                    };
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
