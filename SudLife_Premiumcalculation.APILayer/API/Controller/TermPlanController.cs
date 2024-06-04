using Microsoft.AspNetCore.Mvc;
using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel;
using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel.RequestModel;
using SudLife_Premiumcalculation.APILayer.API.Service.Interface;

namespace SudLife_Premiumcalculation.APILayer.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermPlanController : ControllerBase
    {
        public static IConsumeApiService _consumeAPI;

        public TermPlanController(IConsumeApiService consumeAPI)
        {
            _consumeAPI = consumeAPI;
        }

        [HttpPost]
        public IActionResult Calculate(TermPlanRequestModel RequestBody)
        {
            //Calculte Applicant Date
            DateTime DOB = DateTime.ParseExact(RequestBody.ApplicantDetails.ApplicantDateOfBirth, "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            int Age = CalculateAge(DOB);

            List<Model.ServiceModel.Dummy> Dummy = RequestBody.AddField.Dummy;

            if (RequestBody == null)
            {
                return BadRequest("You can't null value in payload.");
            }


            if (RequestBody.SumAssured >= 500000 && RequestBody.SumAssured <= 2500000 &&  string.IsNullOrEmpty(RequestBody.BenefitOption) )
            {

                if (RequestBody.SumAssured % 50000 == 0)
                {
                    SaralJeevanRequestModel product1 = new SaralJeevanRequestModel();
                    var url = "https://api.example.com/data"; // Replace with your API URL from appsetting 
                    var headers = new Dictionary<string, string>
                    {
                    { "Authorization", "Bearer your_token_here" }
                    };
                    var data = _consumeAPI.ConsumeAPI<SaralJeevanRequestModel>(HttpMethod.Post, url, product1, headers);
                    return Ok(data);

                }
                else
                {
                    BadRequest("Value of Sum Assured for SUD Life Protect Shield  should be incremental of 50 thousands starting from 5 Lakhs. Kindly revise the Sum Assured");
                }
            }
            else if ((RequestBody.SumAssured == 5000000 && RequestBody.BenefitOption == "Life Cover") || (RequestBody.SumAssured >= 5000000 && RequestBody.SumAssured <= 15000000 && RequestBody.BenefitOption == "Life Cover with Return of Premium") || (RequestBody.SumAssured >= 5000000 && RequestBody.SumAssured <= 50000000 && RequestBody.BenefitOption == "Life Cover with Critical Illness"))
            {
                if (RequestBody.SumAssured % 2500000 == 0)
                {
                    ProtectShieldModel product1 = new ProtectShieldModel();
                    var url = "https://api.example.com/data"; // Replace with your API URL from appsetting 
                    var headers = new Dictionary<string, string>
                    {
                    { "Authorization", "Bearer your_token_here" }
                    };
                    var data = _consumeAPI.ConsumeAPI<ProtectShieldModel>(HttpMethod.Post, url, product1, headers);
                    return Ok(data);

                }
                else
                {
                    BadRequest("Value of Sum Assured for SUD Life Protect Shield Plus should be incremental of 25 Lakhs starting from 50 Lakhs. Kindly revise the Sum Assured");
                }

            }
            else if (RequestBody.SumAssured >= 10000000 && RequestBody.SumAssured <= 20000000 && string.IsNullOrEmpty(RequestBody.BenefitOption))
            {
                if (RequestBody.SumAssured % 2500000 == 0)
                {


                }
                else
                {
                    BadRequest("Value of Sum Assured for SUD Life Protect Shield Plus should be incremental of 25 Lakhs starting from 1 Cr. Kindly revise the Sum Assured");
                }
            }
            else
            {
                return BadRequest("Kindly revise the Sum Assured");

            }

            return Ok(RequestBody);
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }


    }
}
