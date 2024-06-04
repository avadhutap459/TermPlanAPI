using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudlife_ProtectShieldPlus.APILayer.API.Global.Filter;
using Sudlife_ProtectShieldPlus.APILayer.API.Model;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.Common;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.ProtectShieldPlus;

namespace Sudlife_ProtectShieldPlus.APILayer.API.Controller
{
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class ProtectShieldPlusController : ControllerBase
    {
        private readonly IProtectShieldPlusSvc _protectShieldPlusSvc;

        public ProtectShieldPlusController(IProtectShieldPlusSvc protectShieldPlusSvc)
        {
            this._protectShieldPlusSvc = protectShieldPlusSvc;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationResultFilter))]
        public  IActionResult ProtectShieldPlus(ProtectShieldPlusRequest objPremiumRequest)
        {
            ProtectShieldPlusResponse objPremiumResponse = new ProtectShieldPlusResponse();

            objPremiumResponse =  _protectShieldPlusSvc.ProtectShieldPlus(objPremiumRequest);

            BaseResponse baseResponse = new BaseResponse();
            if (objPremiumResponse.Status == "Success")
            {
                baseResponse.StatusCode = 200;
                List<string> Msgstr = new List<string>();
                Msgstr.Add("Success");
                baseResponse.Message = Msgstr;
                baseResponse.IsSuccess = true;
                baseResponse.Data = _JsonConvert.SerializeObject(objPremiumResponse);
                return Ok(baseResponse);
            }
            else
            {

                baseResponse.StatusCode = 400;
                List<string> Msgstr = new List<string>();
                Msgstr.Add("Fail");
                baseResponse.Message = Msgstr;
                baseResponse.IsSuccess = false;
                baseResponse.Data = _JsonConvert.SerializeObject(objPremiumResponse);
                return BadRequest(baseResponse);

            }
        }
    }
}
