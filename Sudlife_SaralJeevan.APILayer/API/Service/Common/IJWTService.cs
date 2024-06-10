using Sudlife_SaralJeevan.APILayer.API.Model;

namespace Sudlife_SaralJeevan.APILayer.API.Service.Common
{
    public interface IJWTService
    {
       
        //UserModel AuthenticateUser(UserModel login);
        bool ValidateToken(string token);
        //string GenerateToken(UserModel userInfo);

        //string GenerateToken(string Source, string CustomerId);
        //Task<dynamic> CheckSource(string Source);

        Task<CertDetails> GetCertDetails(string Source);

    }
}
