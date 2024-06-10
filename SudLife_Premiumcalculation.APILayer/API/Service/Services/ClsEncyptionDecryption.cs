using SudLife_Premiumcalculation.APILayer.API.Service.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SudLife_Premiumcalculation.APILayer.API.Service.Services
{
    public class ClsEncyptionDecryption : IDisposable, IEncryptionNDecryption
    {
        bool disposed = false;

        ~ClsEncyptionDecryption()
        {
            Dispose(false);
        }

        public string DataToBeEncrypt(string Plaintxt, string key)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();

                aes.BlockSize = 128;

                aes.KeySize = 256;

                aes.Mode = CipherMode.CBC;

                aes.Padding = PaddingMode.PKCS7;

                byte[] IVBytes16Value = new byte[16];

                byte[] KeyArrBytes32Value = new byte[32];

                aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 32));

                aes.GenerateIV();

                ICryptoTransform encrypto = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] plainTextByte = ASCIIEncoding.UTF8.GetBytes(Plaintxt);

                byte[] CipherText = encrypto.TransformFinalBlock(plainTextByte, 0, plainTextByte.Length);

                return Convert.ToBase64String(CipherText);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public string DataToBeDecrypt(string Encrypttxt, string key)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();

                aes.BlockSize = 128;

                aes.KeySize = 256;

                aes.Mode = CipherMode.CBC;

                aes.Padding = PaddingMode.PKCS7;

                aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 32));

                byte[] iv = new byte[aes.BlockSize / 8];

                byte[] encryptedBytes = Convert.FromBase64CharArray(Encrypttxt.ToCharArray(), 0, Encrypttxt.Length);

                byte[] cipherText = new byte[encryptedBytes.Length - iv.Length];

                Array.Copy(encryptedBytes, iv, iv.Length);

                Array.Copy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

                aes.IV = iv;

                ICryptoTransform decrypto = aes.CreateDecryptor(aes.Key, aes.IV);


                byte[] decryptedData = decrypto.TransformFinalBlock(cipherText, 0, cipherText.Length);

                return ASCIIEncoding.UTF8.GetString(decryptedData);
            }
            catch (Exception ex)
            {
                throw;
            }
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
