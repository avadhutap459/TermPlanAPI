using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Sudlife_SaralJeevan.APILayer.API.Global.Filter;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;

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
        [ServiceFilter(typeof(ValidationFilter))]
        public IActionResult SaralJeevan(SaralJeevanRequest ObjReq)
        {
            BaseResponse baseResponse = new BaseResponse();
            SaralJeevanResponse objsaraljeevanresponse = new SaralJeevanResponse();

            objsaraljeevanresponse = _saralJeevanSvc.SaralJeevan(ObjReq);

            if (objsaraljeevanresponse.Status == "Success")
            {
                baseResponse.StatusCode = 200;
                List<string> Msgstr = new List<string>();
                Msgstr.Add("Success");
                baseResponse.Message = Msgstr;
                baseResponse.IsSuccess = true;
                baseResponse.Data = _JsonConvert.SerializeObject(objsaraljeevanresponse);
                return Ok(baseResponse);
            }
            else
            {

                baseResponse.StatusCode = 400;
                List<string> Msgstr = new List<string>();
                Msgstr.Add("Fail");
                baseResponse.Message = Msgstr;
                baseResponse.IsSuccess = false;
                baseResponse.Data = _JsonConvert.SerializeObject(objsaraljeevanresponse);
                return BadRequest(baseResponse);

            }




        }
    }
}
