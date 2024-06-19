
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.LayoutRenderers.Wrappers;
using System.Transactions;

namespace Sudlife_SaralJeevan.APILayer.API.Global.Filter
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ILogger<JWTValidationFilter> _logger;

        private readonly IJWTService _IJWTService;

        private readonly CommonOperations _CommonOperations;

        private readonly DigitallySignedResponse _DigitallySignedResponse;

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public CustomAuthorizationFilter(IJWTService IJWTService, CommonOperations commonOperations, ILogger<JWTValidationFilter> logger, DigitallySignedResponse digitallySignedResponse)
        {
            _IJWTService = IJWTService;
            _CommonOperations = commonOperations;
            _logger = logger;
            _DigitallySignedResponse = digitallySignedResponse;

        }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            #region Variable Declaration

            EncRequest objEncRequest = new EncRequest();
            EncResponse objEncResponse = new EncResponse();
            BaseResponse objbaseresponse = new BaseResponse();
            string EncryptRequestBody = string.Empty;
            string DecryptRequestBody = string.Empty;



            string ActionName = string.Empty;
            string ControllerName = string.Empty;
            string HmacChecksum = string.Empty;
            string DecryptedData = string.Empty;
            bool ValidToken = false;
            string _GetPayloadData = string.Empty;
            string[] _strRequest = null;
            Microsoft.Extensions.Primitives.StringValues OrignalChecksum;
            dynamic OrgPayLoad = null;
            string strData = string.Empty;
            string _GetSignData = string.Empty;
            byte[] _ByteSignData = null;
            byte[] _BytePayloadData = null;
            bool verify = false;
            string EncryptResponse = string.Empty;
            string PlainResponse = string.Empty;

            #endregion



            try
            {

                //string body = ReadBodyAsString(context.HttpContext.Request);

                //var BankCheckSum = context.HttpContext.Request.Headers["x-sudlife-hmac"].ToString();


                if (filterContext != null)
                {


                    ActionName = (string)filterContext.RouteData.Values["action"];
                    ControllerName = (string)filterContext.RouteData.Values["controller"];
                    filterContext.HttpContext.Request.Headers.TryGetValue("x-sudlife-hmac", out OrignalChecksum);

                    filterContext.HttpContext.Request.EnableBuffering();
                    filterContext.HttpContext.Request.Body.Position = 0;
                    var RequestBodyJsonString = new StreamReader(filterContext.HttpContext.Request.Body).ReadToEnd();
                    RequestBodyJsonString = RequestBodyJsonString.Replace("\r", "");

                    EncryptRequestBody = RequestBodyJsonString;
                    objEncRequest = _JsonConvert.DeSerializeObject<EncRequest>(EncryptRequestBody);

                    if (!string.IsNullOrEmpty(objEncRequest.Source))
                    {
                        if (objEncRequest.Source != "BOI_NewTerm")
                        {

                            _strRequest = _CommonOperations.GetFinalPayLoad(objEncRequest.EncryptReqSignValue);
                            _GetPayloadData = _strRequest[1];
                            _GetSignData = _strRequest[2];
                            OrgPayLoad = JsonConvert.DeserializeObject(_JsonConvert.SerializeObject(_CommonOperations.Decode(_GetPayloadData)));
                            _ByteSignData = Convert.FromBase64String(_GetSignData);
                            _BytePayloadData = Convert.FromBase64String(_GetPayloadData);
                            JToken parsedJson = JToken.Parse(OrgPayLoad);

                            DecryptRequestBody = parsedJson.ToString(Formatting.None);

                          



                            #region CheckSum Validation


                            string CalCheckSum = _CommonOperations.ComputeHashFromJson((DecryptRequestBody));
                            string BankChecksum = OrignalChecksum;
                            if (!BankChecksum.Equals(CalCheckSum))
                            {
                                objbaseresponse = new BaseResponse
                                {
                                    StatusCode = 400,
                                    Message = "Invalid Checksum",
                                    IsSuccess = false,
                                    Data = null,
                                };
                               
                                PlainResponse = _JsonConvert.SerializeObject(objbaseresponse);

                                objEncResponse = _DigitallySignedResponse.GenerateDigitalResponse(PlainResponse, objEncRequest.Source);


                                EncryptResponse = _JsonConvert.SerializeObject(objEncResponse);
                                filterContext.Result = new ContentResult
                                {

                                    Content = Convert.ToString(_JsonConvert.SerializeObject(objEncResponse)),
                                    ContentType = "application/json",
                                    StatusCode = 400,


                                };

                                return;
                            }

                            #endregion

                            #region Token Validation

                            var JWTToken = objEncRequest.AuthToken;
                            ValidToken = _IJWTService.ValidateToken(JWTToken);

                            if (!ValidToken)
                            {
                                objbaseresponse = new BaseResponse
                                {
                                    StatusCode = 401,
                                    Message = "Invalid token / Not matched",
                                    IsSuccess = false,
                                    Data = null,
                                };

                                PlainResponse = _JsonConvert.SerializeObject(objbaseresponse);
                                objEncResponse = _DigitallySignedResponse.GenerateDigitalResponse(PlainResponse, objEncRequest.Source);


                                EncryptResponse = _JsonConvert.SerializeObject(objEncResponse);

                                filterContext.Result = new ContentResult
                                {

                                    Content = Convert.ToString(_JsonConvert.SerializeObject(objEncResponse)),
                                    ContentType = "application/json",
                                    StatusCode = 401,


                                };
                                return;
                            }

                            #endregion

                            #region DigitalSign Validation
                            verify = _CommonOperations.VerifySignatureSource(_BytePayloadData, _ByteSignData, objEncRequest.Source);

                            if (!verify)
                            {
                                objbaseresponse = new BaseResponse
                                {
                                    StatusCode = 400,
                                    Message = "Invalid Signature / Not matched",
                                    IsSuccess = false,
                                    Data = null,
                                };
                                

                                PlainResponse = _JsonConvert.SerializeObject(objbaseresponse);
                                objEncResponse = _DigitallySignedResponse.GenerateDigitalResponse(PlainResponse, objEncRequest.Source);

                                EncryptResponse = _JsonConvert.SerializeObject(objEncResponse);

                                filterContext.Result = new ContentResult
                                {

                                    Content = Convert.ToString(_JsonConvert.SerializeObject(objEncResponse)),
                                    ContentType = "application/json",
                                    StatusCode = 400,


                                };
                                return;
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        objbaseresponse = new BaseResponse
                        {
                            StatusCode = 400,
                            Message = "Please enter valid Source",
                            IsSuccess = false,
                            Data = null,
                        };

                        
                        filterContext.Result = new ContentResult
                        {

                            Content = Convert.ToString(_JsonConvert.SerializeObject(objbaseresponse)),
                            ContentType = "application/json",
                            StatusCode = 400,


                        };
                        return;
                    }

                }

                return;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


    }



    public class CustomAuthorizationAttribute : TypeFilterAttribute
    {
        public CustomAuthorizationAttribute() : base(typeof(CustomAuthorizationFilter)) { }
    }
}
