using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudlife_ProtectShieldPlus.APILayer.API.Model;
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
        public async Task<ProtectShieldPlusResponse> ProtectShieldPlus(ProtectShieldPlusRequest objPremiumRequest)
        {
            ProtectShieldPlusResponse objPremiumResponse=new ProtectShieldPlusResponse();

            objPremiumResponse = await _protectShieldPlusSvc.ProtectShieldPlus(objPremiumRequest);

            return objPremiumResponse;
        }
    }
}
