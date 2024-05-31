using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;

namespace Sudlife_SaralJeevan.APILayer.API.Service
{
    public class CommonOperation
    {
        private readonly IConfiguration _configuration;
       
        
        public CommonOperation(IConfiguration configuration)
        {
            _configuration = configuration;
           
          
        }
        public string ConvertToYears(string PolicyTerm)
        {
            try
            {
                int Loantenureinyears = Convert.ToInt32(PolicyTerm) / 12;
                return PolicyTerm = Convert.ToString(Loantenureinyears);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string DateFormating(string DateToBeFormatted)
        {
            try
            {
                string FormattedDate = string.Empty;
                if (string.IsNullOrEmpty(DateToBeFormatted))
                    return null;
                FormattedDate = DateTime.ParseExact(DateToBeFormatted, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                return FormattedDate;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string CalculateAge(DateTime dateOfBirth)
        {
            try
            {
                int age = 0;
                age = DateTime.Now.Year - dateOfBirth.Year;
                if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                    age = age - 1;

                return age.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string APIKey()
        {
            try
            {
                string Key = string.Empty;
                dynamic _responseJson = string.Empty;

                Key = _configuration.GetSection("KEYS:APIKey").Value;
                return Key;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string Gender(string Gendr)
        {
            Dictionary<string, string> DictGender = new Dictionary<string, string>();
            DictGender.Add("male", "M");
            DictGender.Add("female", "F");
            DictGender.Add("third gender", "T");
            DictGender.Add("trans", "T");
            DictGender.Add("others", "O");
            string Gen = DictGender[Gendr].ToString();
            return Gen;
        }

        public string StandardAgeProof(string SAP)
        {
            Dictionary<string, string> DictSAP = new Dictionary<string, string>();
            DictSAP.Add("yes", "1");
            DictSAP.Add("no", "0");
            string SAPs = DictSAP[SAP].ToString();
            return SAPs;
        }

        public string ConsumeSDEAPI(string RequestBody, string serviceType)
        {
            try
            {
                string Url = string.Empty;
                dynamic _responseJson = string.Empty;
                Url = _configuration.GetSection("URLS:ServiceURL").Value;
                       
                using (HttpClientHandler Handler = new HttpClientHandler())
                using (HttpClient client = new HttpClient(Handler))
                {
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var content = new StringContent(RequestBody, Encoding.UTF8, "application/json");

                    var APIResponse = client.PostAsync(Url, content).Result;

                    _responseJson = APIResponse.Content.ReadAsStringAsync().Result;

                }
                return _responseJson;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
