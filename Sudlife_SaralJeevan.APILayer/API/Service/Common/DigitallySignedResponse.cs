using NLog;
using Sudlife_SaralJeevan.APILayer.API.Database;
using Sudlife_SaralJeevan.APILayer.API.Model;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace Sudlife_SaralJeevan.APILayer.API.Service.Common
{
    public class DigitallySignedResponse
    {
        private readonly IConfiguration _configuration;
        private readonly CommonOperations _CommonOperations;
        private readonly IGenericRepo _IGenericRepo;
        private readonly ILogger<GenericRepo> _logger;

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public DigitallySignedResponse(IConfiguration configuration, ILogger<GenericRepo> logger, CommonOperations commonOperations, IGenericRepo genericRepo)
        {
            _configuration = configuration;
            _CommonOperations = commonOperations;
            _IGenericRepo = genericRepo;
            _logger = logger;
        }


        public async Task<byte[]> SignDataSource(byte[] data, string source)
        {
            try
            {
                string Env = _configuration.GetSection("URLS:Env").Value;

                PathResponse pathResponse = new PathResponse();

                pathResponse = await _IGenericRepo.GetPathDetails(source, Env, "Private");

                string pathvalue = pathResponse.KeyPath;


                string PrivateCertiPassword = _configuration.GetSection("KEYS:PrivateCertiPassword").Value;

                X509Certificate2 privateCertificate = new X509Certificate2(pathvalue, PrivateCertiPassword);

                using (var sha256 = SHA256.Create())
                {

                    using (var rsa = privateCertificate.GetRSAPrivateKey())
                    {
                        return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string Encode(string serverName)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(serverName));
        }

        public static string Decode(string encodedServername)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }

        public static string JwtHeaderInBase64()
        {
            string JsonHeader = "{'typ':'JWT','alg':'RS256'}";

            return Encode(JsonHeader);
        }

        public static string PayloadInBase64(string ReceivedRequest)
        {
            string JsonPayload = ReceivedRequest;

            return Encode(JsonPayload);
        }

        public string EncryptData(string DecryptTxt, string key)
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



                // aes.Key = keyArr;
                aes.Key = KeyArrBytes32Value;
                aes.IV = IVBytes16Value;



                ICryptoTransform encrypto = aes.CreateEncryptor();



                byte[] plainTextByte = ASCIIEncoding.UTF8.GetBytes(DecryptTxt);
                byte[] CipherText = encrypto.TransformFinalBlock(plainTextByte, 0, plainTextByte.Length);
                return Convert.ToBase64String(CipherText);
            }
            catch (Exception)
            {



                throw;
            }

        }


        public async Task<string> DigitalsignSource(string ReceivedRequest, string Source)
        {
            string header = JwtHeaderInBase64();

            string payload = PayloadInBase64(ReceivedRequest);


            string JsonPayload = ReceivedRequest;

            var plainTextBytes1 = System.Text.Encoding.UTF8.GetBytes(JsonPayload);


            byte[] _signdata = await SignDataSource(plainTextBytes1, Source);

            string SignDataInBase64 = Convert.ToBase64String(_signdata);

            #region Concat Header + Payload + SignData

            string FinalPayloadWith_Header_Payload_SignData = header + "." + payload + "." + SignDataInBase64;

            #endregion

            string Encode_FinalPayloadWith_Header_Payload_SignData = Encode(FinalPayloadWith_Header_Payload_SignData);


            string Encrypt_FinalPayload = EncryptData(Encode_FinalPayloadWith_Header_Payload_SignData, _CommonOperations.GetKey());

            return Encrypt_FinalPayload;
        }

        public async Task<EncResponse> GenerateDigitalResponse(dynamic objResponse, string source)
        {
            EncResponse response = new EncResponse();
            response.EncryptResponseSignValue = await this.DigitalsignSource(objResponse, source);
            response.CheckSum = _CommonOperations.ComputeHashFromJson(objResponse);
            return response;

        }

    }
}
