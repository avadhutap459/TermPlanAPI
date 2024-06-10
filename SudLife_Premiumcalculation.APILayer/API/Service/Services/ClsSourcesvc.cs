using SudLife_Premiumcalculation.APILayer.API.Database;
using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel;
using SudLife_Premiumcalculation.APILayer.API.Service.Interface;
using System.Data;
using Dapper;

namespace SudLife_Premiumcalculation.APILayer.API.Service.Services
{
    public class ClsSourcesvc : IDisposable, ISource
    {
        DbConnection ConnectionManager;
        bool disposed = false;

        public ClsSourcesvc()
        {
            ConnectionManager = DbConnection.SingleInstance;
        }
        ~ClsSourcesvc()
        {
            Dispose(false);
        }


        public ClsSource Getsourcedetailsbaseonsourcename(string sourcename)
        {
            try
            {
                ClsSource Objsource = new ClsSource();

                using (IDbConnection cn = ConnectionManager.connection)
                {
                    cn.Open();

                    Objsource = cn.Query<ClsSource>("select * from [dbo].[mstSource] where SourceName = @SourceName " +
                        "and IsActive = 1", new
                        {
                            SourceName = sourcename
                        }).FirstOrDefault();
                }

                return Objsource;
            }
            catch(Exception ex)
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
