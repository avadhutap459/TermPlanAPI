using Microsoft.AspNetCore.Mvc;
using SudLife_Premiumcalculation.APILayer.API.Global.Filter;
using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel;
using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel.RequestModel;
using SudLife_Premiumcalculation.APILayer.API.Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace SudLife_Premiumcalculation.APILayer.API.Controller
{
    public class Urnoinvestor
    {
        [Required]
        public string Urno { get; set; } = string.Empty;
    }
    [Route("v1")]
    [ApiController]
    public class TermPlanController : ControllerBase
    {
        private ITokengeneration Token { get; set; }
        private IEncryptionNDecryption EncryptDecrypt { get; set; }
        public TermPlanController(ITokengeneration token, IEncryptionNDecryption encryptDecrypt)
        {
            Token = token;
            EncryptDecrypt = encryptDecrypt;
        }

        [HttpPost("BadResponse")]
        public IActionResult BadResponse(ClsBadResponseM Objbadres)
        {
            try
            {
                return BadResponse(Objbadres);
            }
            catch(Exception ex)
            {
                throw;
            }
        }



        [HttpPost("TermPlanCalcuation")]
        [TypeFilter(typeof(ClsCustomManipulationFilter))]
        public IActionResult Calculate([FromBody] Urnoinvestor RequestBody)
        {
            //Calculte Applicant Date
            

            return Ok(new {name = "Hello Avadhut"});
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        [HttpGet("GenerateToken")]
        public IActionResult GenerateToken()
        {
            try
            {
                string _Token = Token.GenerateToken("BOI", "2CEF55EB764E420DAADFD047BE9DAEE1", "123456");

                string _EncryptToken = EncryptDecrypt.DataToBeEncrypt("BOI", _Token, "BF6886467F3E4984BE219BC8010F382D");

                return Ok(new { Token = _Token, EncryptToken = _EncryptToken});
            }
            catch(Exception ex)
            {
                throw;
            }
        }


    }
}
