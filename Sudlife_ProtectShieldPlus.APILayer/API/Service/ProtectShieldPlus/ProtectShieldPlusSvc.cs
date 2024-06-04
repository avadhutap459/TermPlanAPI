using Newtonsoft.Json;
using NLog;
using Sudlife_ProtectShieldPlus.APILayer.API.Database;
using Sudlife_ProtectShieldPlus.APILayer.API.Model;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.Common;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.DynamicParams;

namespace Sudlife_ProtectShieldPlus.APILayer.API.Service.ProtectShieldPlus
{
    public class ProtectShieldPlusSvc : IProtectShieldPlusSvc
    {
        public readonly CommonOperations _CommonOperations;
        public readonly DynamicCollections _dynamiccoll;

        public readonly IGenericRepo _IGenericRepo;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public ProtectShieldPlusSvc(CommonOperations CommonOperations, DynamicCollections dynamiccoll, IGenericRepo IGenericRepo)
        {
            _CommonOperations = CommonOperations;
            _dynamiccoll = dynamiccoll;
            _IGenericRepo = IGenericRepo;

        }


        public   dynamic ProtectShieldPlus(ProtectShieldPlusRequest objProtectShieldPlusRequest)
        {
            int Logid = 0;
            try
            {
                string DecRequest = _JsonConvert.SerializeObject(objProtectShieldPlusRequest);

                int SourceId = 1;
                int ProductID = 3;
                string AdminName = "Admin";

                dynamic Insert =  _IGenericRepo.SaveServiceLog("Insert", SourceId, 0, DecRequest, "", AdminName, "", ProductID);              
                Logid = Convert.ToInt32(Insert[0]);

                #region ValueAssignment




                ProtectShieldPlusResponse objPremiumResponse = new ProtectShieldPlusResponse();
                string ProposerDOB = _CommonOperations.DateFormating(DateToBeFormatted: objProtectShieldPlusRequest.ProposerDetails.ProposerDateOfBirth);

                string ApplicantDOB = _CommonOperations.DateFormating(DateToBeFormatted: objProtectShieldPlusRequest.ApplicantDetails.ApplicantDateOfBirth);

                int ApplicantAge = Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(ApplicantDOB)));

                int ProposerAge = Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(ProposerDOB)));

                string LIGender = _CommonOperations.Gender(objProtectShieldPlusRequest.ApplicantDetails.ApplicantGender.ToLower());

                string ProposerGender = _CommonOperations.Gender(objProtectShieldPlusRequest.ProposerDetails.ProposerGender.ToLower());




                string PolicyTerm = _CommonOperations.ConvertToYears(Convert.ToString(objProtectShieldPlusRequest.PolicyTerm));

                string PremiumPaymentTerm = _dynamiccoll.PremiumPaymentTermProtectShieldPlus(objProtectShieldPlusRequest.PremiumPaymentTerm.ToLower(), PolicyTerm);

                string InputMode = _dynamiccoll.InputMode(objProtectShieldPlusRequest.PremiumPaymentModes.ToLower());

                string DistributionChannel = _dynamiccoll.DistributionChannelProtectShieldPlus(objProtectShieldPlusRequest.DistributionChannel.ToLower());

                int SumAssured = objProtectShieldPlusRequest.SumAssured;
                if (SumAssured < 10000000)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Minimum Sum Assured for SUD Life Protect Shield Plus is 10000000.00. Kindly revise the sum assured.";
                    return objPremiumResponse;

                }

                if (SumAssured > 20000000)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Maximum Sum Assured for SUD Life Protect Shield Plus is 20000000.00. Kindly revise the sum assured.";
                    return objPremiumResponse;

                }

                if (ApplicantAge < 18)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Entry age for SUD Life Protect Sheild Plus can not be less than 18 Years. Kindly revise the age";
                    return objPremiumResponse;
                }

                if (ApplicantAge > 60)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Maximum entry age for SUD Life Protect Sheild Plus is 60 years. Kindly revise the age";
                    return objPremiumResponse;
                }

                #endregion


                #region ListRequestCreation


                List<SDEBaseKeyValuePair> lstkeyvalue = new List<SDEBaseKeyValuePair>
            {
                new SDEBaseKeyValuePair { key = "@LI_FNAME", value = objProtectShieldPlusRequest.ApplicantDetails.ApplicantFName },
                new SDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                new SDEBaseKeyValuePair { key = "@LI_LNAME", value =objProtectShieldPlusRequest.ApplicantDetails.ApplicantLName },

                new SDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value = Convert.ToString(ApplicantAge) },
                new SDEBaseKeyValuePair { key = "@LI_DOB", value =ApplicantDOB},
                new SDEBaseKeyValuePair { key = "@LI_GENDER", value =LIGender },


                new SDEBaseKeyValuePair { key = "@LI_STATE", value = "9" },
                new SDEBaseKeyValuePair { key = "@LI_CITY", value = "142" },
                new SDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =objProtectShieldPlusRequest.ProposerDetails.ProposerFName },


                new SDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                new SDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = objProtectShieldPlusRequest.ProposerDetails.ProposerLName },
                new SDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(ProposerAge) },


                new SDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = ProposerDOB },
                new SDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = ProposerGender },
                new SDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                new SDEBaseKeyValuePair { key = "@SameProposer", value = objProtectShieldPlusRequest.ProposerDetails.IsProposersameasApplicant},
                new SDEBaseKeyValuePair { key = "@INPUT_MODE", value = InputMode},
                new SDEBaseKeyValuePair { key = "@PR_ID", value = "1038"},

                new SDEBaseKeyValuePair { key = "@PR_PT", value = PolicyTerm },
                new SDEBaseKeyValuePair { key = "@PR_PPT", value = PremiumPaymentTerm },
                new SDEBaseKeyValuePair { key = "@PR_ANNPREM", value = string.Empty},


                new SDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                new SDEBaseKeyValuePair { key = "@PR_SA", value = Convert.ToString(objProtectShieldPlusRequest.SumAssured)},

                new SDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},


                new SDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},



                new SDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},


                new SDEBaseKeyValuePair { key = "@PR_OPTION_1", value = DistributionChannel},
                new SDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},


        };

                #endregion



                #region Rider

                List<Rider> riderList = new List<Rider>();

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
                    riders = new List<Rider>(riderList)
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

                        objPremiumResponse.ModalPremiumwithTax = (objPremiumResponse.ModalPremium + objPremiumResponse.Tax);

                        objPremiumResponse.AnnualPremium = InputValidationStatus[0].AnnualPremium;

                        objPremiumResponse.Message = jsonString2.Message;

                        objPremiumResponse.Status = jsonString2.Status;

                        objPremiumResponse.TransactionId = jsonString2.TransactionId;

                        objPremiumResponse.TotalPremium = objPremiumResponse.ModalPremium;

                        objPremiumResponse.TotalTAX = objPremiumResponse.Tax;

                        objPremiumResponse.TotalPremiumwithTax = objPremiumResponse.ModalPremiumwithTax;

                        objPremiumResponse.TotalAnnualPremium = objPremiumResponse.AnnualPremium;


                    }
                }
                else
                {

                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";

                }


                string DecResponse = _JsonConvert.SerializeObject(objPremiumResponse);
               
                dynamic Update =  _IGenericRepo.SaveServiceLog("Update", SourceId, Logid, "", DecResponse, "", "Admin", ProductID);

                return objPremiumResponse;
            }
            catch (Exception ex)
            {
                int a = Logid;
                var ErrorResult =  _IGenericRepo.SaveErrorLog(a, ex.ToString());

                throw ex;

            }
        }

    }
}
