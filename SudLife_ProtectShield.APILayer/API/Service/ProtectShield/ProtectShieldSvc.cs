
using Newtonsoft.Json;
using NLog;
using SudLife_ProtectShield.APILayer.API.Database;
using SudLife_ProtectShield.APILayer.API.Model;
using SudLife_ProtectShield.APILayer.API.Service.Common;
using SudLife_ProtectShield.APILayer.API.Service.DynamicParams;
using System.Data;

namespace SudLife_ProtectShield.APILayer.API.Service.ProtectShield
{
    public class ProtectShieldSvc : IProtectShieldSvc
    {
        public readonly CommonOperations _CommonOperations;
        public readonly DynamicCollections _dynamiccoll;
        public readonly IGenericRepo _IGenericRepo;

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public ProtectShieldSvc(CommonOperations CommonOperations, DynamicCollections dynamiccoll, IGenericRepo IGenericRepo)
        {
            _CommonOperations = CommonOperations;
            _dynamiccoll = dynamiccoll;
            _IGenericRepo = IGenericRepo;

        }

        public dynamic ProtectShield(ProtectShieldRequest objProtectShieldRequest)
        {
            int Logid = 0;
            try
            {
                string DecRequest = _JsonConvert.SerializeObject(objProtectShieldRequest);

                int SourceId = 1;
                int ProductID = 2;
                string AdminName = "Admin";

                dynamic Insert = _IGenericRepo.SaveServiceLog("Insert", SourceId, 0, DecRequest, "", AdminName, "", ProductID);

                Logid = Convert.ToInt32(Insert[0]);


                #region ValueAssignment

                ProtectShieldResponse objPremiumResponse = new ProtectShieldResponse();
                string ProposerDOB = _CommonOperations.DateFormating(DateToBeFormatted: objProtectShieldRequest.ProposerDetails.ProposerDateOfBirth);

                string ApplicantDOB = _CommonOperations.DateFormating(DateToBeFormatted: objProtectShieldRequest.ApplicantDetails.ApplicantDateOfBirth);

                int ApplicantAge = Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(ApplicantDOB)));

                int ProposerAge = Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(ProposerDOB)));

                string LIGender = _CommonOperations.Gender(objProtectShieldRequest.ApplicantDetails.ApplicantGender.ToLower());

                string ProposerGender = _CommonOperations.Gender(objProtectShieldRequest.ProposerDetails.ProposerGender.ToLower());

                string StandardAgeProof = _CommonOperations.StandardAgeProof(objProtectShieldRequest.StandardAgeProof.ToLower());



                string PolicyTerm = _CommonOperations.ConvertToYears(Convert.ToString(objProtectShieldRequest.PolicyTerm));

                string PremiumPaymentTerm = _dynamiccoll.PremiumPaymentTermProtectShield(objProtectShieldRequest.PremiumPaymentTerm.ToLower(), PolicyTerm);

                string InputMode = _dynamiccoll.InputMode(objProtectShieldRequest.PremiumPaymentModes.ToLower());

                string StaffPolicy = _dynamiccoll.StaffPolicyProtectShield(objProtectShieldRequest.StaffPolicy.ToLower());

                string DistributionChannel = _dynamiccoll.DistributionChannelProtectShield(objProtectShieldRequest.DistributionChannel.ToLower());

                string Smoker = _dynamiccoll.Smoker(objProtectShieldRequest.Smoke.ToLower());

                string BenefitOption = _dynamiccoll.BenefitOption(objProtectShieldRequest.BenefitOption.ToLower());

                string PayoutOption = _dynamiccoll.PayoutOptionProtectShield(objProtectShieldRequest.PayoutOption.ToLower());

                if (ApplicantAge < 18)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Entry age for SUD Life Protect Sheild can not be less than 18 Years. Kindly revise the age";
                    return objPremiumResponse;

                }

                if (ApplicantAge > 55)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Maximum entry age for SUD Life Protect Sheild is 55 years. Kindly revise the age";
                    return objPremiumResponse;

                }

                #endregion


                #region ListRequestCreation

                List<SDEBaseKeyValuePair> lstkeyvalue = new List<SDEBaseKeyValuePair>
                {
                    new SDEBaseKeyValuePair { key = "@LI_FNAME", value = objProtectShieldRequest.ApplicantDetails.ApplicantFName },
                    new SDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                    new SDEBaseKeyValuePair { key = "@LI_LNAME", value =objProtectShieldRequest.ApplicantDetails.ApplicantLName },

                    new SDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value = Convert.ToString(ApplicantAge) },
                    new SDEBaseKeyValuePair { key = "@LI_DOB", value =ApplicantDOB},
                    new SDEBaseKeyValuePair { key = "@LI_GENDER", value =LIGender },


                    new SDEBaseKeyValuePair { key = "@LI_STATE", value = "9" },
                    new SDEBaseKeyValuePair { key = "@LI_CITY", value = "142" },
                    new SDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =objProtectShieldRequest.ProposerDetails.ProposerFName },


                    new SDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                    new SDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = objProtectShieldRequest.ProposerDetails.ProposerLName },
                    new SDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(ProposerAge) },


                    new SDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = ProposerDOB },
                    new SDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = ProposerGender },
                    new SDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                    new SDEBaseKeyValuePair { key = "@SameProposer", value = objProtectShieldRequest.ProposerDetails.IsProposersameasApplicant},
                    new SDEBaseKeyValuePair { key = "@INPUT_MODE", value = InputMode},
                    new SDEBaseKeyValuePair { key = "@PR_ID", value = "1034"},

                    new SDEBaseKeyValuePair { key = "@PR_PT", value = PolicyTerm },
                    new SDEBaseKeyValuePair { key = "@PR_PPT", value = PremiumPaymentTerm },
                    new SDEBaseKeyValuePair { key = "@PR_ANNPREM", value = string.Empty},


                    new SDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                    new SDEBaseKeyValuePair { key = "@PR_SA", value = Convert.ToString(objProtectShieldRequest.SumAssured)},

                    new SDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},


                    new SDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},
                    new SDEBaseKeyValuePair { key = "@NSAP_FLAG", value = StandardAgeProof },



                    new SDEBaseKeyValuePair { key = "@LI_SMOKE", value = Smoker},

                    new SDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},

                    new SDEBaseKeyValuePair { key = "@PR_OPTION_1", value = BenefitOption},
                    new SDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},

                    new SDEBaseKeyValuePair { key = "@PR_OPTION_2", value = PayoutOption},
                    new SDEBaseKeyValuePair { key = "@OPTION_VALUE_2", value = ""},


                    new SDEBaseKeyValuePair { key = "@PR_OPTION_3", value = DistributionChannel},
                    new SDEBaseKeyValuePair { key = "@OPTION_VALUE_3", value = ""},

                    new SDEBaseKeyValuePair { key = "@PR_OPTION_4", value = StaffPolicy},
                    new SDEBaseKeyValuePair { key = "@OPTION_VALUE_4", value = ""},



            };


                #endregion


                #region Rider

                string Rider1Value = Convert.ToString(objProtectShieldRequest.ADTPDRiderSA);

                if (objProtectShieldRequest.ADTPDRiderOpted.ToLower() == "no" || objProtectShieldRequest.ADTPDRiderOpted == "")
                {
                    Rider1Value = "";
                }

                Rider objrider = new Rider();
                objrider.RiderId = 2001;
                List<FormInput> f3 = new List<FormInput>();
                f3.Add(new FormInput() { key = "@RD_SA", value = Rider1Value });
                objrider.formInputs = f3;

                List<Rider> riderList = new List<Rider>();

                if (Rider1Value == "")
                {
                    riderList = new List<Rider>();
                }

                if (Rider1Value != "")
                {
                    riderList = new List<Rider>();
                    riderList.Add(objrider);
                }


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

                        if (Rider1Value != "")
                        {
                            objPremiumResponse.AATPDPremium = InputValidationStatus[1].ModalPremium;

                            objPremiumResponse.AATPDTax = InputValidationStatus[1].Tax;

                            objPremiumResponse.AATPDwithTax = (objPremiumResponse.AATPDPremium + objPremiumResponse.AATPDTax);

                            objPremiumResponse.TotalPremium = (objPremiumResponse.ModalPremium + objPremiumResponse.AATPDPremium);

                            objPremiumResponse.TotalTAX = (objPremiumResponse.Tax + objPremiumResponse.AATPDTax);

                            objPremiumResponse.TotalPremiumwithTax = (objPremiumResponse.ModalPremiumwithTax + objPremiumResponse.AATPDwithTax);


                            objPremiumResponse.AATPDAnnualPremium = InputValidationStatus[1].AnnualPremium;

                            objPremiumResponse.TotalAnnualPremium = (objPremiumResponse.AnnualPremium + objPremiumResponse.AATPDAnnualPremium);

                        }
                    }
                }
                else
                {

                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";

                }


                string DecResponse = _JsonConvert.SerializeObject(objPremiumResponse);

                dynamic Update = _IGenericRepo.SaveServiceLog("Update", SourceId, Logid, "", DecResponse, "", "Admin", ProductID);

                return objPremiumResponse;
            }
            catch (Exception ex)
            {
                int a = Logid;
                var ErrorResult = _IGenericRepo.SaveErrorLog(a, ex.ToString());

                throw ex;
            }


        }
    }
}

