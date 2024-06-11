using SecurityMechansim.ServiceLayer.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SecurityMechansim.ServiceLayer.Service
{
    public class ClsChecksum : IChecksum, IDisposable
    {
        bool disposed = false;

        ~ClsChecksum()
        {
            Dispose(false);
        }

        public string Generatechecksumusingplaintext(string sourcename , string plaintext,string keyforchecksum)
        {
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(keyforchecksum);
                byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
                HMACSHA256 hashstring = new HMACSHA256(key);
                byte[] hash = hashstring.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
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
