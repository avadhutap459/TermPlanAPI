using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Sudlife_SaralJeevan.APILayer.API.Global.Filter;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Sudlife_SaralJeevan.APILayer.API.Controller
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class SaralJeevanController : ControllerBase
    {
        private readonly ISaralJeevanSvc _saralJeevanSvc;
        private readonly CommonOperations _CommonOperations;
        private readonly DigitallySignedResponse _DigitallySignedResponse;
        public SaralJeevanController(ISaralJeevanSvc saralJeevanSvc, CommonOperations commonOperations, DigitallySignedResponse digitallySignedResponse)
        {
            _CommonOperations = commonOperations;
            _saralJeevanSvc = saralJeevanSvc;
            _DigitallySignedResponse = digitallySignedResponse;
        }

        [HttpPost]


        [ServiceFilter(typeof(CustomAuthorizationFilter))]


        public IActionResult SaralJeevan(SaralJeevanRequest ObjReq)
        {
            SaralJeevanRequest objSaralJeevanrequest = new SaralJeevanRequest();
            SaralJeevanResponse objsaraljeevanresponse = new SaralJeevanResponse();
            BaseResponse baseResponse = new BaseResponse();
            EncResponse objEncResponse = new EncResponse();
            Request.EnableBuffering();
            Request.Body.Position = 0;
            var RequestBodyJsonString = new StreamReader(Request.Body).ReadToEnd();


            EncRequest ObjEncReq = _JsonConvert.DeSerializeObject<EncRequest>(RequestBodyJsonString);

            if (ObjEncReq.Source != "BOI_NewTerm")
            {

                string[] _strRequest = _CommonOperations.GetFinalPayLoad(ObjEncReq.EncryptReqSignValue);
                string _GetPayloadData = _strRequest[1];
                dynamic OrgPayLoad = JsonConvert.DeserializeObject(_JsonConvert.SerializeObject(_CommonOperations.Decode(_GetPayloadData)));
                objSaralJeevanrequest = JsonConvert.DeserializeObject<SaralJeevanRequest>(OrgPayLoad);
                objsaraljeevanresponse = _saralJeevanSvc.SaralJeevan(objSaralJeevanrequest);

                var res = _JsonConvert.SerializeObject(objsaraljeevanresponse);
                objEncResponse = _DigitallySignedResponse.GenerateDigitalResponse(res, ObjEncReq.Source);
                return Ok(objEncResponse);
            }
            else
            {
                objSaralJeevanrequest = _JsonConvert.DeSerializeObject<SaralJeevanRequest>(RequestBodyJsonString);

                #region ModelValidation

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(objSaralJeevanrequest, serviceProvider: null, items: null);
                bool isValid = Validator.TryValidateObject(objSaralJeevanrequest, validationContext, validationResults, validateAllProperties: true);
                if (!isValid)
                {
                    var errors = validationResults.SelectMany(vr => vr.MemberNames.Select(memberName => new ValidationError
                    {
                        PropertyName = memberName,
                        ErrorMessage = vr.ErrorMessage
                    })).ToList();
                    var ValidationErrors = JsonConvert.SerializeObject(errors);
                    JsonConvert.DeserializeObject(ValidationErrors);
                   
                    baseResponse = new BaseResponse
                    {  
                        Message = "Bad Request",
                        Data = ValidationErrors,
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        IsSuccess = false,
                    };

                    return BadRequest(baseResponse);

                }
                #endregion



                objsaraljeevanresponse = _saralJeevanSvc.SaralJeevan(objSaralJeevanrequest);
                if (objsaraljeevanresponse.Status == "Success")
                {
                    baseResponse = new BaseResponse
                    {
                        StatusCode = 200,
                        Message = "Success",
                        IsSuccess = true,
                        Data = _JsonConvert.SerializeObject(objsaraljeevanresponse),
                    };
                    return Ok(baseResponse);
                }
                else
                {
                    baseResponse = new BaseResponse
                    {
                        StatusCode = 400,
                        Message = "Fail",
                        IsSuccess = false,
                        Data = _JsonConvert.SerializeObject(objsaraljeevanresponse),
                    };
                    return BadRequest(baseResponse);

                }
            }








        }

        
    }
}
