using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Newtonsoft.Json;

namespace Sudlife_SaralJeevan.APILayer.API.Global.Filter
{
    public class DigitalSignFilter : IAsyncActionFilter
    {
        private readonly IJWTService _IJWTService;

        private readonly CommonOperations _CommonOperations;

        private readonly ILogger<JWTValidationFilter> _logger;

        private readonly DigitallySignedResponse _DigitallySignedResponse;

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public DigitalSignFilter(IJWTService IJWTService, CommonOperations commonOperations, ILogger<JWTValidationFilter> logger, DigitallySignedResponse digitallySignedResponse)
        {
            _IJWTService = IJWTService;
            _CommonOperations = commonOperations;
            _logger = logger;
            _DigitallySignedResponse = digitallySignedResponse;
            _DigitallySignedResponse = digitallySignedResponse;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                #region Variable Declaration
                BaseResponse objbaseresponse = new BaseResponse();
                EncRequest objEncRequest = new EncRequest();
                EncResponse objEncResponse = new EncResponse();
                dynamic ObjReq = "";
                string strData = string.Empty;
                string _GetSignData = string.Empty;
                string _GetPayloadData = string.Empty;
                string[] _strRequest = null;
                byte[] _ByteSignData = null;
                byte[] _BytePayloadData = null;
                bool verify = false;
                #endregion

                if (context.ModelState.IsValid)
                {


                    foreach (var argument in context.ActionArguments.Values)
                    {
                        ObjReq = _JsonConvert.DeSerializeObject<dynamic>(_JsonConvert.SerializeObject(argument));

                    }

                    if (ObjReq.Source == "BOI_NewTerm")
                    {
                        await next();
                    }
                    else
                    {

                        foreach (var argument in context.ActionArguments.Values)
                        {


                            objEncRequest = _JsonConvert.DeSerializeObject<EncRequest>(_JsonConvert.SerializeObject(argument));

                        }


                        strData = objEncRequest.EncryptReqSignValue;
                        #region Retrieve request payload and decrypt

                        _strRequest = _CommonOperations.GetFinalPayLoad(strData);
                        _GetSignData = _strRequest[2];
                        _GetPayloadData = _strRequest[1];
                        _ByteSignData = Convert.FromBase64String(_GetSignData);
                        _BytePayloadData = Convert.FromBase64String(_GetPayloadData);
                        // verify = _CommonOperations.VerifySignature(_BytePayloadData, _ByteSignData);
                        verify =await _CommonOperations.VerifySignatureSource(_BytePayloadData, _ByteSignData, objEncRequest.Source);
                        if (verify)
                        {
                           
                            await next();
                           
                        }
                        if (!verify)
                        {
                            logger.Info("Invalid Signature / Not matched\"");
                            List<string> Msgstr = new List<string>();
                            Msgstr.Add("Invalid Signature / Not matched\"");
                            objbaseresponse.Message = Msgstr;
                            objbaseresponse.StatusCode = 400;
                            objbaseresponse.IsSuccess = false;

                            var InvalidSignature = _JsonConvert.SerializeObject(objbaseresponse);
                            objEncResponse.EncryptResponseSignValue = await _DigitallySignedResponse.DigitalsignSource(InvalidSignature, objEncRequest.Source);
                            objEncResponse.CheckSum = _CommonOperations.ComputeHashFromJson(InvalidSignature);
                            context.Result = new ContentResult
                            {
                                Content = Convert.ToString(_JsonConvert.SerializeObject(objEncResponse)),
                                ContentType = "application/json",
                                StatusCode = 400
                            };

                        }
                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }



        }
    }
}
