﻿using SecurityMechansim.ServiceLayer.Interface;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace SecurityMechansim.ServiceLayer.Service
{
    public class ClsDigitalSigning : IDisposable, IDigitalSign
    {
        bool disposed = false;
        ClsEncyptionDecryption Objencryptdecryptsvc = new ClsEncyptionDecryption();

        public ClsDigitalSigning()
        {

        }
        ~ClsDigitalSigning()
        {
            Dispose(false);
        }

        public string DataToBeDigitallySign(string plaintxt,string sourcename,string privatefilepath,string privatefilepassword,string encryptderyptkey)
        {
            try
            {
                string header = JwtHeaderInBase64();

                string payload = PayloadInBase64(plaintxt);

                var plaintxtinbyteformat = System.Text.Encoding.UTF8.GetBytes(plaintxt);
                
                byte[] _signdata = SignData(plaintxtinbyteformat, privatefilepath, privatefilepassword);
                
                string SignDataInBase64 = Convert.ToBase64String(_signdata);

                #region Concat Header + Payload + SignData

                string FinalPayloadWith_Header_Payload_SignData = header + "." + payload + "." + SignDataInBase64;

                #endregion

                string Encode_FinalPayloadWith_Header_Payload_SignData = Encode(FinalPayloadWith_Header_Payload_SignData);

                string Encrypt_FinalPayload = Objencryptdecryptsvc.DataToBeEncrypt(sourcename,Encode_FinalPayloadWith_Header_Payload_SignData, encryptderyptkey);

                return Encrypt_FinalPayload;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public bool DataToBeValidateDigitalSign(byte[] _bytesigndata, byte[] _bytepayloaddata,string publicfilepath)
        {
            try
            {
                return VerifySignature(_bytesigndata, _bytepayloaddata, publicfilepath);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private bool VerifySignature(byte[] data, byte[] signature,string publicfilepath)
        {
            X509Certificate2 publiccertifcate = new X509Certificate2(publicfilepath);

            using (var sha256 = SHA256.Create())
            {
                using (var rsa = publiccertifcate.GetRSAPublicKey())
                {
                    return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
        }

        private byte[] SignData(byte[] data, string filepath,string filepassword)
        {
            try
            {
                X509Certificate2 privateCertificate = new X509Certificate2(filepath, filepassword);

                using (var sha256 = SHA256.Create())
                {

                    using (var rsa = privateCertificate.GetRSAPrivateKey())
                    {
                        return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    }

                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        private string JwtHeaderInBase64()
        {
            string JsonHeader = "{'typ':'JWT','alg':'RS256'}";

            return Encode(JsonHeader);
        }
        private string PayloadInBase64(string ReceivedRequest)
        {
            string JsonPayload = ReceivedRequest;

            return Encode(JsonPayload);
        }
        private string Encode(string serverName)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(serverName));
        }
        private string Decode(string encodedServername)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                // Console.WriteLine("This is the first call to Dispose. Necessary clean-up will be done!");

                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    // Console.WriteLine("Explicit call: Dispose is called by the user.");
                }
                else
                {
                    // Console.WriteLine("Implicit call: Dispose is called through finalization.");
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Console.WriteLine("Unmanaged resources are cleaned up here.");

                // TODO: set large fields to null.

                disposedValue = true;
            }
            else
            {
                // Console.WriteLine("Dispose is called more than one time. No need to clean up!");
            }
        }



        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }


        #endregion
    }
}
