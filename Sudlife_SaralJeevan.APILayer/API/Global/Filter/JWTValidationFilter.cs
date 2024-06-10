using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using Microsoft.AspNetCore.Mvc;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace Sudlife_SaralJeevan.APILayer.API.Global.Filter
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // This method runs before the action method
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' is starting.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method runs after the action method
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' has completed.");
        }
    }
    public class JWTValidationFilter : IAsyncActionFilter
    {

        private readonly ILogger<JWTValidationFilter> _logger;

        private readonly IJWTService _IJWTService;

        private readonly CommonOperations _CommonOperations;

        private readonly DigitallySignedResponse _DigitallySignedResponse;

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public JWTValidationFilter(IJWTService IJWTService, CommonOperations commonOperations, ILogger<JWTValidationFilter> logger, DigitallySignedResponse digitallySignedResponse)
        {
            _IJWTService = IJWTService;
            _CommonOperations = commonOperations;
            _logger = logger;
            _DigitallySignedResponse = digitallySignedResponse;

        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            #region Variable Declaration
            dynamic ObjReq = "";
            EncRequest objEncRequest = new EncRequest();
            EncResponse objEncResponse = new EncResponse();
            BaseResponse objbaseresponse = new BaseResponse();
            
            string HmacChecksum = string.Empty;
            string DecryptedData = string.Empty;
            bool ValidToken = false;
            string _GetPayloadData = string.Empty;
            string[] _strRequest = null;

            dynamic OrgPayLoad = null;
            #endregion
            try
            {

                var BankCheckSum = context.HttpContext.Request.Headers["x-sudlife-hmac"].ToString();

                foreach (var argument in context.ActionArguments.Values)
                {


                    ObjReq = _JsonConvert.DeSerializeObject<SaralJeevanRequest>(_JsonConvert.SerializeObject(argument));

                }

                string Source = ObjReq.Source;
                if (Source == "BOI_NewTerm")
                {
                    await next();
                }
                else
                {
                    foreach (var argument in context.ActionArguments.Values)
                    {


                        objEncRequest = _JsonConvert.DeSerializeObject<EncRequest>(_JsonConvert.SerializeObject(argument));

                    }

                    _strRequest = _CommonOperations.GetFinalPayLoad(objEncRequest.EncryptReqSignValue);
                    _GetPayloadData = _strRequest[1];
                    OrgPayLoad = JsonConvert.DeserializeObject(_JsonConvert.SerializeObject(_CommonOperations.Decode(_GetPayloadData)));

                    
                    JToken parsedJson = JToken.Parse(OrgPayLoad);
                    var minified = parsedJson.ToString(Formatting.None);
                    logger.Info("MnifiedJson" + minified);
                    var actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;




                    var JWTToken = objEncRequest.AuthToken;

                    ValidToken = _IJWTService.ValidateToken(JWTToken);



                    logger.Info("JWTToken status:-" + ValidToken);

                    HmacChecksum = _CommonOperations.ComputeHashFromJson(minified);


                    if (ValidToken)
                    {
                        if (BankCheckSum.Equals(HmacChecksum))
                        {
                            logger.Info("Checksum Equals");
                            await next();
                        }
                        else
                        {
                            List<string> Msgstr = new List<string>();
                            Msgstr.Add("Invalid Checksum");
                            objbaseresponse.Message = Msgstr;
                            objbaseresponse.StatusCode = 400;
                            objbaseresponse.IsSuccess = false;
                            var InvalidChecksum = _JsonConvert.SerializeObject(objbaseresponse);


                            objEncResponse.EncryptResponseSignValue = await _DigitallySignedResponse.DigitalsignSource(InvalidChecksum, objEncRequest.Source);
                            objEncResponse.CheckSum = _CommonOperations.ComputeHashFromJson(InvalidChecksum);
                            context.Result = new ContentResult
                            {

                                Content = Convert.ToString(_JsonConvert.SerializeObject(objEncResponse)),
                                ContentType = "application/json",
                                StatusCode = 400,


                            };
                            logger.Info("Invalid Checksum");
                        }
                    }
                    else if (!ValidToken)
                    {
                        List<string> Msgstr = new List<string>();
                        Msgstr.Add("Invalid token / Not matched");
                        objbaseresponse.Message = Msgstr;
                        objbaseresponse.StatusCode = 401;
                        objbaseresponse.IsSuccess = false;

                        var Invalidtoken = _JsonConvert.SerializeObject(objbaseresponse);
                        objEncResponse.EncryptResponseSignValue = await _DigitallySignedResponse.DigitalsignSource(Invalidtoken, objEncRequest.Source);
                        objEncResponse.CheckSum = _CommonOperations.ComputeHashFromJson(Invalidtoken);
                        context.Result = new ContentResult
                        {
                            Content = Convert.ToString(_JsonConvert.SerializeObject(objEncResponse)),
                            ContentType = "application/json",
                            StatusCode = 401
                        };
                    }
                }




            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
