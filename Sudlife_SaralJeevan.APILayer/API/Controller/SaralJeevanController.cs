using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service;

namespace Sudlife_SaralJeevan.APILayer.API.Controller
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class SaralJeevanController : ControllerBase
    {
        private readonly ISaralJeevanSvc _saralJeevanSvc;
        public SaralJeevanController(ISaralJeevanSvc saralJeevanSvc)
        {
            _saralJeevanSvc = saralJeevanSvc;
        }

        [HttpPost]
        public async Task<SaralJeevanResponse> SaralJeevan(SaralJeevanRequest ObjReq)
        {

            SaralJeevanResponse objresponse = new SaralJeevanResponse();
            objresponse = await _saralJeevanSvc.SaralJeevan(ObjReq);
            return objresponse;
        }
    }
}
