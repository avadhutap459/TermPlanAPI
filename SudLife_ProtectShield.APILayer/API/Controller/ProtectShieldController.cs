using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SudLife_ProtectShield.APILayer.API.Model;
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
        public async Task<ProtectShieldResponse> ProtectShield(ProtectShieldRequest Objrequest)
        {

            ProtectShieldResponse objPremiumResponse = new ProtectShieldResponse();
            objPremiumResponse = await _protectShieldSvc.ProtectShield(Objrequest);

            return objPremiumResponse;
        }
    }
}
