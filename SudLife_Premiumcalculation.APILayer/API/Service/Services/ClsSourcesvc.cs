using SudLife_Premiumcalculation.APILayer.API.Database;
using System.Data;
using Dapper;
using SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel;

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


        public bool Checksourceexistornotbaseonsourcename(string sourcename)
        {
            try
            {
                bool Issourceexit;

                using (IDbConnection cn = ConnectionManager.connection)
                {
                    cn.Open();

                    Issourceexit = cn.ExecuteScalar<int>("select count(1) from [dbo].[mstSource] where SourceName = @SourceName " +
                        "and IsActive = 1", new
                        {
                            SourceName = sourcename
                        }) > 1;
                }

                return Issourceexit;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<int> Getsecuritymechdatabaseonsourcename(string sourcename,string type)
        {
            try
            {
                List<int> lstsecurityIds = new List<int>();

                using (IDbConnection cn = ConnectionManager.connection)
                {
                    cn.Open();

                    lstsecurityIds = cn.Query<int>("select ,SM.SecurityID from [dbo].[mstSecurityMech] SM " +
                        "Inner Join [dbo].[txnSourceMapwithSecurity] SMS ON SM.SecurityID = SMS.SecurityID " +
                        "Left Join [dbo].[mstSource] MS ON SMS.SourceId = MS.SourceId " +
                        "where SM.Type = @type and SM.IsActive = 1 and MS.SourceName = @sourcename " +
                        "Order by SMS.orderId asc", new
                        {
                            @type = type,
                            @sourcename = sourcename
                        }).ToList();
                }

                return lstsecurityIds;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public ClsSource Getsourcedetailsbaseonsourcename(string sourcename)
        {
            try
            {
                ClsSource objsource = new ClsSource();
                using (IDbConnection cn = ConnectionManager.connection)
                {
                    cn.Open();

                    objsource = cn.Query<ClsSource>("select * from [dbo].[mstSource] where SourceName = SourceName",
                        new
                        {
                            SourceName = sourcename
                        }).FirstOrDefault();
                }
               return objsource;
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
