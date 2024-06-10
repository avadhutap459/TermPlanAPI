using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sudlife_SaralJeevan.APILayer.API.Database;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Sudlife_SaralJeevan.APILayer.API.Service.DynamicParams;

namespace Sudlife_SaralJeevan.APILayer.API.Service
{
    public class SaralJeevanSvc : ISaralJeevanSvc
    {
        public readonly CommonOperations _CommonOperations;
        public readonly DynamicCollections _dynamiccoll;

        private readonly IGenericRepo _IGenericRepo;

        public SaralJeevanSvc(CommonOperations CommonOperations, DynamicCollections dynamiccoll, IGenericRepo IGenericRepo)
        {
            _CommonOperations = CommonOperations;
            _dynamiccoll = dynamiccoll;
            _IGenericRepo = IGenericRepo;


        }

        public dynamic SaralJeevan(SaralJeevanRequest objPremiumRequest)
        {
            int LogId = 0;
            try
            {
                string DecRequest = _JsonConvert.SerializeObject(objPremiumRequest);
                string AdminName = "Admin";
                int SourceId = 1;
                int ProductId = 1;
                dynamic Insert = _IGenericRepo.SaveServiceLog("Insert", SourceId, 0, DecRequest, "", AdminName, "", ProductId);
                LogId = Convert.ToInt32(Insert[0]);
                SaralJeevanResponse objPremiumResponse = new SaralJeevanResponse();
                #region ValueAssignment


                string ProposerDOB = _CommonOperations.DateFormating(DateToBeFormatted: objPremiumRequest.ProposerDetails.ProposerDateOfBirth);

                string ApplicantDOB = _CommonOperations.DateFormating(DateToBeFormatted: objPremiumRequest.ApplicantDetails.ApplicantDateOfBirth);

                int ApplicantAge = Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(ApplicantDOB)));

                int ProposerAge = Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(ProposerDOB)));

                string LIGender = _CommonOperations.Gender(objPremiumRequest.ApplicantDetails.ApplicantGender.ToLower());

                string ProposerGender = _CommonOperations.Gender(objPremiumRequest.ProposerDetails.ProposerGender.ToLower());

                string StandardAgeProof = _CommonOperations.StandardAgeProof(objPremiumRequest.StandardAgeProof.ToLower());

                string PremiumPaymentTerm = _CommonOperations.ConvertToYears(Convert.ToString(objPremiumRequest.PremiumPaymentTerm));

                string PolicyTerm = _CommonOperations.ConvertToYears(Convert.ToString(objPremiumRequest.PolicyTerm));

                string InputMode = _dynamiccoll.InputMode(objPremiumRequest.PremiumPaymentModes.ToLower());

                string StaffPolicy = _dynamiccoll.StaffPolicySaralJeevanBima(objPremiumRequest.StaffPolicy.ToLower());

                string DistributionChannel = _dynamiccoll.DistributionChannelSaralJeevanBima(objPremiumRequest.DistributionChannel.ToLower());


                #endregion


                #region ListRequestCreation



                List<SDEBaseKeyValuePair> lstkeyvalue = new List<SDEBaseKeyValuePair>
            {
                new SDEBaseKeyValuePair { key = "@LI_FNAME", value = objPremiumRequest.ApplicantDetails.ApplicantFName.ToString() },
                new SDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                new SDEBaseKeyValuePair { key = "@LI_LNAME", value =objPremiumRequest.ApplicantDetails.ApplicantLName },

                new SDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value =  Convert.ToString(ApplicantAge) },
                new SDEBaseKeyValuePair { key = "@LI_DOB", value = ApplicantDOB},
                new SDEBaseKeyValuePair { key = "@LI_GENDER", value =LIGender },


                new SDEBaseKeyValuePair { key = "@LI_STATE", value = "19" },
                new SDEBaseKeyValuePair { key = "@LI_CITY", value = "65" },
                new SDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =objPremiumRequest.ProposerDetails.ProposerFName },


                new SDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                new SDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = objPremiumRequest.ProposerDetails.ProposerLName },
                new SDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(ProposerAge) },


                new SDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = ProposerDOB },
                new SDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = ProposerGender },
                new SDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                new SDEBaseKeyValuePair { key = "@SameProposer", value = objPremiumRequest.ProposerDetails.IsProposersameasApplicant },
                new SDEBaseKeyValuePair { key = "@INPUT_MODE", value = InputMode },
                new SDEBaseKeyValuePair { key = "@PR_ID", value = "1029"},

                new SDEBaseKeyValuePair { key = "@PR_PT", value = PolicyTerm },

               new SDEBaseKeyValuePair { key = "@PR_PPT", value = PremiumPaymentTerm},

                new SDEBaseKeyValuePair { key = "@PR_ANNPREM", value = ""},

                new SDEBaseKeyValuePair { key = "@PR_SA", value =Convert.ToString (objPremiumRequest.SumAssured)},


                new SDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                new SDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},

                new SDEBaseKeyValuePair { key = "@NSAP_FLAG", value = StandardAgeProof},


                new SDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},

                new SDEBaseKeyValuePair { key = "@kfc", value = "0"},

                new SDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},

                new SDEBaseKeyValuePair { key = "@PR_OPTION_1", value = DistributionChannel},

                new SDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},

                new SDEBaseKeyValuePair { key = "@PR_OPTION_2", value = StaffPolicy},

                new SDEBaseKeyValuePair { key = "@OPTION_VALUE_2", value = ""},



        };


                #endregion


                #region ConsumeAPI
                string APIKey = _CommonOperations.APIKey();

                SDEBaseRequest root = new SDEBaseRequest
                {
                    APIKey = APIKey,
                    formInputs = new List<SDEBaseKeyValuePair>(lstkeyvalue),
                    inputPartialWithdrawal = new List<InputPW>(),
                    inputOptions = new List<InputOptions>(),
                    funds = new List<FundInput>(),
                    riders = new List<Rider>()
                };


                var jsonString = _JsonConvert.SerializeObject(root);
                var APIRequest = jsonString.Replace("null", "[]");

                DateTime RequestTime = DateTime.Now;

                dynamic ResponseFromNsureservice = _CommonOperations.ConsumeSDEAPI(APIRequest, "premium");


                #endregion

                DateTime ResponseTime = DateTime.Now;
                if (!string.IsNullOrEmpty(ResponseFromNsureservice))
                {
                    var jsonString2 = _JsonConvert.DeSerializeObject<SDEBaseResponse>(ResponseFromNsureservice);
                    if (jsonString2.Status == "Fail" || jsonString2.BIJson == null)
                    {
                        objPremiumResponse.Status = jsonString2.Status;
                        objPremiumResponse.Message = jsonString2.Message;
                        objPremiumResponse.TransactionId = jsonString2.TransactionId;

                        if (jsonString2.InputValidationStatus != null)
                        {
                            if (jsonString2.InputValidationStatus[0].IpKwMessage.Count > 0)
                            {
                                objPremiumResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].IpKwMessage[0];
                            }
                            else
                            {
                                objPremiumResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].GeneralError;
                            }
                        }


                    }
                    else
                    {
                        var InputValidationStatus = jsonString2.InputValidationStatus;


                        objPremiumResponse.ModalPremium = InputValidationStatus[0].ModalPremium;

                        objPremiumResponse.Tax = InputValidationStatus[0].Tax;

                        objPremiumResponse.ModalPremiumwithTax = Math.Round(objPremiumResponse.ModalPremium + objPremiumResponse.Tax);

                        objPremiumResponse.Message = jsonString2.Message;

                        objPremiumResponse.TransactionId = jsonString2.TransactionId;

                        objPremiumResponse.Status = jsonString2.Status;

                    }
                }
                else
                {

                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";

                }

                string DecResponse = _JsonConvert.SerializeObject(objPremiumResponse);

                dynamic Update = _IGenericRepo.SaveServiceLog("Update", SourceId, LogId, "", DecResponse, "", "Admin", ProductId);

                return objPremiumResponse;
            }
            catch (Exception ex)
            {
                int a = LogId;
                var ErrorResult = _IGenericRepo.SaveErrorLog(a, ex.ToString());
                throw ex;
            }
        }

    }
}
