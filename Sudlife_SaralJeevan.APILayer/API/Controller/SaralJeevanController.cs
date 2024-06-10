using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Sudlife_SaralJeevan.APILayer.API.Global.Filter;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Newtonsoft.Json;

namespace Sudlife_SaralJeevan.APILayer.API.Controller
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class SaralJeevanController : ControllerBase
    {
        private readonly ISaralJeevanSvc _saralJeevanSvc;
        private readonly CommonOperations _CommonOperations;
        public SaralJeevanController(ISaralJeevanSvc saralJeevanSvc,CommonOperations commonOperations)
        {
            _CommonOperations = commonOperations;
            _saralJeevanSvc = saralJeevanSvc;
        }

        [HttpPost]
        [ServiceFilter(typeof(JWTValidationFilter))]
        [ServiceFilter(typeof(DigitalSignFilter))]
        [ServiceFilter(typeof(ValidationFilter))]
        

        public IActionResult SaralJeevan([FromBody] SaralJeevanRequest ObjReq)
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

        [HttpPost]
        [ServiceFilter(typeof(JWTValidationFilter))]
        [ServiceFilter(typeof(DigitalSignFilter))]
        [ServiceFilter(typeof(ValidationFilter))]


        public IActionResult SaralJeevanBima(EncRequest ObjReq)
        {
            BaseResponse baseResponse = new BaseResponse();
            SaralJeevanResponse objsaraljeevanresponse = new SaralJeevanResponse();
            SaralJeevanRequest objrequest = new SaralJeevanRequest();
            string[] _strRequest = _CommonOperations.GetFinalPayLoad(ObjReq.EncryptReqSignValue);
            string _GetPayloadData = _strRequest[1];
            dynamic OrgPayLoad = JsonConvert.DeserializeObject(_JsonConvert.SerializeObject(_CommonOperations.Decode(_GetPayloadData)));
            objrequest = JsonConvert.DeserializeObject<SaralJeevanRequest>(OrgPayLoad);
            objsaraljeevanresponse = _saralJeevanSvc.SaralJeevan(objrequest);

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
