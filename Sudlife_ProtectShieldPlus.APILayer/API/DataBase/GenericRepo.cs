using Dapper;
using NLog;
using System.Data;
using System.Data.SqlClient;

namespace Sudlife_ProtectShieldPlus.APILayer.API.Database
{
    public class GenericRepo : IGenericRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        string connection = string.Empty;
        string PrivateKey = string.Empty;


        private readonly ILogger<GenericRepo> _logger;

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public GenericRepo(IConfiguration configuration, ILogger<GenericRepo> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DBConnection");

        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<dynamic> SaveServiceLog(string Flag, int SourceId, int LogId, string PlainReq, string PlainRes, string createdBy, string LastModifiedBy, int ProductId)
        {
            string spName = "[dbo].[StpSaveServiceLog]";


            using (IDbConnection con = CreateConnection())
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@LogId", LogId);
                    param.Add("@SourceId", SourceId);
                    param.Add("@ProductId", ProductId);
                    param.Add("@PlainReq", PlainReq);
                    param.Add("@PlainRes", PlainRes);
                    param.Add("@Flag", Flag);
                    param.Add("@createdBy", createdBy);
                    param.Add("@LastModifiedBy", LastModifiedBy);
                    var result = (await con.QueryAsync<int>(spName, param, commandType: System.Data.CommandType.StoredProcedure, commandTimeout: 60)).FirstOrDefault();


                    return result;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public async Task<string> SaveErrorLog( int Logid, string ErrorDescription)
        {
            string connection = _configuration.GetSection("ConnectionStrings:DBConnection").Value;
            string spName = "[dbo].[StpSaveErrorLogs]";

            using (IDbConnection con = CreateConnection())
            {

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {

                    var param = new DynamicParameters();
                    param.Add("@Logid", Logid);
                    param.Add("@CreatedBy", "Admin");                  
                    param.Add("@ErrorDescription", ErrorDescription);

                    var result = await con.QueryAsync<string>(spName, param, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    return "Success";
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
        }

    }
}
