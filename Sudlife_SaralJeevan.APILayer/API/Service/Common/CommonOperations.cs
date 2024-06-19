using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using Sudlife_SaralJeevan.APILayer.API.Database;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Sudlife_SaralJeevan.APILayer.API.Service
{
    public class CommonOperations
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommonOperations> _logger;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IGenericRepo _IGenericRepo;
        public CommonOperations(IConfiguration configuration, IGenericRepo genericRepo, ILogger<CommonOperations> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _IGenericRepo = genericRepo;
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

        public string[] GetFinalPayLoad(string DecodedString)
        {
            string Decrypt_FinalPayload = DecryptData(DecodedString, GetKey());

            string Decode_FinalPayloadWith_Header_Payload_SignData = Decode(Decrypt_FinalPayload);

            return Decode_FinalPayloadWith_Header_Payload_SignData.Split('.');
        }

        public string GetKey()
        {
            return _configuration.GetSection("KEYS:EncryptDecryptKey").Value;
        }
        public string DecryptData(string EncryptedText, string key)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                byte[] keyArr = Convert.FromBase64String(key);
                byte[] ivArr = { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 7, 7, 7, 7 };
                byte[] IVBytes16Value = new byte[16];
                Array.Copy(ivArr, IVBytes16Value, 16);

                byte[] KeyArrBytes32Value = new byte[32];

                Array.Copy(keyArr, KeyArrBytes32Value, 24);

                aes.Key = KeyArrBytes32Value;
                aes.IV = IVBytes16Value;

                ICryptoTransform decrypto = aes.CreateDecryptor();

                byte[] encryptedBytes = Convert.FromBase64CharArray(EncryptedText.ToCharArray(), 0, EncryptedText.Length);
                byte[] decryptedData = decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return ASCIIEncoding.UTF8.GetString(decryptedData);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string Decode(string encodedServername)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }
        public string ComputeHashFromJson(string jsonstr)
        {
            string finaloutput = string.Empty;

            string CheckSumKey = _configuration.GetSection("KEYS:CheckSumKey").Value;

            byte[] key = Encoding.UTF8.GetBytes(CheckSumKey);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonstr);

            HMACSHA256 hashstring = new HMACSHA256(key);
            byte[] hash = hashstring.ComputeHash(bytes);
            finaloutput = Convert.ToBase64String(hash);


            return finaloutput;
        }

        public bool VerifySignatureSource(byte[] data, byte[] signature, string source)
        {
           
            string Env = _configuration.GetSection("URLS:Env").Value;



            string pathvalue = _IGenericRepo.GetPathDetails(source, Env, "Public");

            //string pathvalue = path.KeyPath;

            X509Certificate2 publiccertificate1 = new X509Certificate2(pathvalue);

            using (var sha256 = SHA256.Create())
            {
                using (var rsa = publiccertificate1.GetRSAPublicKey())
                {
                    return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
        }

    }
}
