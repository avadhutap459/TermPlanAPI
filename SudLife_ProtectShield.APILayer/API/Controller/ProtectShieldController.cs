using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SudLife_ProtectShield.APILayer.API.Global.Filter;
using SudLife_ProtectShield.APILayer.API.Model;
using SudLife_ProtectShield.APILayer.API.Service.Common;
using SudLife_ProtectShield.APILayer.API.Service.ProtectShield;

namespace SudLife_ProtectShield.APILayer.API.Controller
{
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class ProtectShieldController : ControllerBase
    {
        private readonly IProtectShieldSvc _protectShieldSvc;

        public ProtectShieldController(IProtectShieldSvc protectShieldSvc)
        {
            this._protectShieldSvc = protectShieldSvc;
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationResultFilter))]
        public IActionResult ProtectShield(ProtectShieldRequest Objrequest)
        {

            ProtectShieldResponse objPremiumResponse = new ProtectShieldResponse();
            objPremiumResponse =  _protectShieldSvc.ProtectShield(Objrequest);
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
