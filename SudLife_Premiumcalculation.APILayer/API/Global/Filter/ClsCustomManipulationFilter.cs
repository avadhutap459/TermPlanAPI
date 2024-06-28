using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel;
using SudLife_Premiumcalculation.APILayer.API.Service.Global;
using System.Net;
using System.Text;

namespace SudLife_Premiumcalculation.APILayer.API.Global.Filter
{


    public class ClsCustomManipulationFilter : IResourceFilter
    {
        private IEncryptionNDecryption EncryptionDecryptionSvc { get; set; }
        private ITokengeneration TokenGenerationSvc { get; set; }
        private ISource SourceSvc { get; set; }
        private MemoryStream newBody;
        private Stream originalBody;
        public ClsCustomManipulationFilter(IEncryptionNDecryption _EncryptionDecryptionSvc, 
            ISource _SourceSvc,
            ITokengeneration _TokenGenerationSvc)
        {
            EncryptionDecryptionSvc = _EncryptionDecryptionSvc;
            SourceSvc = _SourceSvc;
            TokenGenerationSvc = _TokenGenerationSvc;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            newBody.Seek(0, SeekOrigin.Begin);
            using StreamReader sr = new StreamReader(newBody);
            string actionResult = sr.ReadToEnd();

            string fake = actionResult + " Priti Parab";


            byte[] data = GetStreamWithGetBytes(fake, Encoding.UTF8);

            //write modified response to original response
            using MemoryStream memStream = new MemoryStream();
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;
            memStream.CopyToAsync(originalBody).Wait();
            context.HttpContext.Response.Body = originalBody;



        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            try
            {
                ClsRequest Objrequest = new ClsRequest();
                string requestbody = string.Empty;
                string SourceName = string.Empty;
                ClsBadResponseM objbadres;


                var request = context.HttpContext.Request;
                request.EnableBuffering();
                request.Body.Position = 0;

                string actionName = context.RouteData.Values["action"].ToString();
                string controllerName = context.RouteData.Values["controller"].ToString();

                using (var reader = new StreamReader(request.Body))
                {
                    requestbody = reader.ReadToEndAsync().Result;
                }

                string checksumval = context.HttpContext.Request.Headers["x-sudlife-hmac"].ToString();


                if(string.IsNullOrEmpty(requestbody))
                {
                    objbadres = new ClsBadResponseM("It seems that request is not properly formed.", (int)HttpStatusCode.BadRequest, 
                        "Please provide request body in current context", false, "", 
                        DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                else
                {
                    Objrequest = ClsJsonConverter.DeserializeObject<ClsRequest>(requestbody);
                }

                if (string.IsNullOrEmpty(checksumval))
                {
                    objbadres = new ClsBadResponseM("It seems that request is not properly formed.", (int)HttpStatusCode.BadRequest,
                        "Please provide checksum for processing request", false, "",
                        DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                SourceName = Objrequest.Source;

                if (! SourceSvc.Checksourceexistornotbaseonsourcename(SourceName))
                {
                    objbadres = new ClsBadResponseM("It seems that request is not properly formed.", (int)HttpStatusCode.BadRequest,
                        "Source name does not exist or temporarily deactive from databse, Please connect with Sud IT team", false, "",
                        DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                    byte[] bytes = Encoding.ASCII.GetBytes(ClsJsonConverter.SerializeObject(objbadres));
                    request.Body = new MemoryStream(bytes); // here i am trying to change request body

                    context.Result = new RedirectToActionResult("BadResponse", "TermPlan", objbadres);

                    goto Here;

                }



                List<int> lstsecuritymechIds = SourceSvc.Getsecuritymechdatabaseonsourcename(SourceName, "Request");

                ClsSource objsourcedetails = SourceSvc.Getsourcedetailsbaseonsourcename(SourceName);








                switch (actionName)
                {
                    case "TermPlanCalcuation":

                        ClsRequest objRequest = ClsJsonConverter.DeserializeObject<ClsRequest>(requestbody);





                        var decriptedFromJavascript = "{ \"Urno\":\"URN123456\"}"; //{ "Urno":"URN123456"}

                        //request.Body.Position = 0;

                        byte[] bytes = Encoding.ASCII.GetBytes(decriptedFromJavascript);
                        request.Body = new MemoryStream(bytes); // here i am trying to change request body

                        break;
                    default:
                        break;
                }


                 Here:

                newBody = new MemoryStream();
                originalBody = context.HttpContext.Response.Body;
                context.HttpContext.Response.Body = newBody;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private string ReadBodyAsString(HttpRequest request)
        {

            string stringContent = string.Empty;
            try
            {
                request.EnableBuffering();
                request.Body.Position = 0;

                using (var reader = new StreamReader(request.Body))
                {
                    stringContent = reader.ReadToEndAsync().Result;

                    request.Body.Position = 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return stringContent;
        }

        private Tuple<string, ClsBadResponseM> GetPlainRequestAfterSecurityThreadProcess(List<int> LstSecurityIds, 
            string PrivateFilePathForDigitalSign,
            string PrivateFilePassword,
            string PublicFilePath,
            string KeyforEncryptDecrypt,
            string Keyfortoken,
            string Keyforchecksum,
            string Sourcename,
            ClsRequest Objrequest)
        {
            try
            {
                ClsBadResponseM _objbadresponse = null;
                string _getdecrypttoken =string.Empty;
                int _count = 0;

                foreach (int SecurityId in LstSecurityIds)
                {
                    if(_objbadresponse != null && _count <= 1)
                    {
                        break;
                    }

                    switch(SecurityId)
                    {
                        case (int)enumsecuritythread.DecryptToken:

                            _getdecrypttoken = EncryptionDecryptionSvc.DataToBeDecrypt(Sourcename, Objrequest.AuthToken, KeyforEncryptDecrypt);

                            break;
                        case (int)enumsecuritythread.ValidateToken:

                            var _verifytoken = ValidateToken(_getdecrypttoken, Keyfortoken, Sourcename);

                            _objbadresponse = _verifytoken.Item2 as ClsBadResponseM;

                            return new Tuple<string, ClsBadResponseM>("", _objbadresponse);

                            break;
                        case (int)enumsecuritythread.ValidateDigitalSign:
                            break;
                        case (int)enumsecuritythread.DecryptRequest:
                            break;
                        case (int)enumsecuritythread.ValidateChecksum:
                            break;
                        case (int)enumsecuritythread.EncryptResponse:
                            break;
                        case (int)enumsecuritythread.DigitalSign:
                            break;
                        case (int)enumsecuritythread.ChecksumGeneration:
                            break;
                    }
                    _count++;
                }

                return Tuple.Create("", _objbadresponse);
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        private byte[] GetStreamWithGetBytes(string sampleString, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            var byteArray = encoding.GetBytes(sampleString);
            return byteArray;
        }

        private Tuple<bool, ClsBadResponseM> ValidateToken(string Token , string Key, string Sourcename)
        {
            try
            {
                ClsBadResponseM _objbadresponse = null;
                bool isvalid = true;

                Tuple<string,string> _gettoken = TokenGenerationSvc.ValidateToken(Token, Key);

                if (string.IsNullOrEmpty(_gettoken.Item1) || string.IsNullOrEmpty(_gettoken.Item2))
                {
                    _objbadresponse = new ClsBadResponseM("It seems that request is not properly formed.",
                        (int)HttpStatusCode.BadRequest,
                        "Token does not in proper format ",
                        false,
                        "",
                        DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                    isvalid = false;
                }

                if (_gettoken.Item1 != Sourcename)
                {
                    _objbadresponse = new ClsBadResponseM("It seems that request is not properly formed.",
                        (int)HttpStatusCode.BadRequest,
                        "Source available in request and available in token both is different",
                        false,
                        "",
                        DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                    isvalid = false;
                }

                return new Tuple<bool, ClsBadResponseM>(isvalid, _objbadresponse);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }

}
