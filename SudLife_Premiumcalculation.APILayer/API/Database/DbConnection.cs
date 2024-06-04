using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace SudLife_Premiumcalculation.APILayer.API.Database
{
    public sealed class DbConnection
    {
        public static string appDirectory = System.Environment.CurrentDirectory;
        public static string env = string.Empty;
        public static IConfiguration configuration;
        private static DbConnection _singleInstance;
        private static readonly object lockObject = new object();
        private DbConnection()
        {
            var config = new ConfigurationBuilder().SetBasePath(appDirectory)
                                                   .AddJsonFile("appsettings.json")
                                                   .Build();
            env = config.GetSection("Env").Value;

            configuration = new ConfigurationBuilder().SetBasePath(appDirectory)
                                       .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                                       .Build();
        }

        public static DbConnection SingleInstance
        {
            get
            {
                lock (lockObject)
                {
                    if (_singleInstance == null)
                    {
                        _singleInstance = new DbConnection();
                    }

                }
                return _singleInstance;
            }
        }
        public IDbConnection connection => new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}
