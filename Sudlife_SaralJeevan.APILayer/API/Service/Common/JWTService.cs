using NLog;
using Sudlife_SaralJeevan.APILayer.API.Database;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Sudlife_SaralJeevan.APILayer.API.Model;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Sudlife_SaralJeevan.APILayer.API.Service.Common
{
    public class JWTService : IJWTService
    {
        private IConfiguration _config;
        public readonly IGenericRepo _IGenericRepo;

        private readonly ILogger<JWTService> _logger;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public JWTService(IConfiguration config, IGenericRepo IGenericRepo, ILogger<JWTService> logger)
        {
            _config = config;
            _IGenericRepo = IGenericRepo;
            _logger = logger;
        }

       


        public bool ValidateToken(string token)
        {
            if (token == null)
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            try
            {
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _config["JWT:Issuer"],
                    // ValidAudience = _config["JWT:ValidAudience"],
                    IssuerSigningKey = secretKey
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return true;
            }
            catch (Exception ex)
            {
                // return null if validation fails
                return false;
            }
        }

        //public string GenerateToken(UserModel userInfo)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Entered Generate Token");
        //        string Source = userInfo.Source;
        //        string CustomerId = userInfo.CustomerId;
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        //Create a List of Claims, Keep claims name short    
        //        var permClaims = new List<Claim>();
        //        //permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //        //  permClaims.Add(new Claim(CLAIM_USER_NAME, user));

        //        permClaims.Add(new Claim("Source", Source));
        //        permClaims.Add(new Claim("CustomerId", CustomerId));

        //        var tokeOptions = new JwtSecurityToken(issuer: _config["JWT:Issuer"],
        //                // audience: _config["JWT:ValidAudience"],
        //                //claims: new List<Claim>()

        //                expires: DateTime.Now.AddMinutes(30),
        //                signingCredentials: signinCredentials);
        //        string jwt_token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        _logger.LogInformation("Generated Token" + jwt_token);
        //        return jwt_token;


        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.ToString();
        //    }


        //}

        //public string GenerateToken(string Source, string CustomerId)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Entered Generate Token");

        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        //Create a List of Claims, Keep claims name short    
        //        var permClaims = new List<Claim>();
        //        //permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //        //  permClaims.Add(new Claim(CLAIM_USER_NAME, user));

        //        permClaims.Add(new Claim("Source", Source));
        //        permClaims.Add(new Claim("CustomerId", CustomerId));

        //        var tokeOptions = new JwtSecurityToken(issuer: _config["JWT:Issuer"],
        //                // audience: _config["JWT:ValidAudience"],
        //                //claims: new List<Claim>()

        //                expires: DateTime.Now.AddMinutes(30),
        //                signingCredentials: signinCredentials);
        //        string jwt_token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        _logger.LogInformation("Generated Token" + jwt_token);
        //        return jwt_token;


        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.ToString();
        //    }


        //}
        //public async Task<dynamic> CheckSource(string Source)
        //{
        //    try
        //    {
        //        logger.Info("Entered CheckSource Service");
        //        bool result = false;
        //        BaseResponse response = new BaseResponse();
        //        response = await _IGenericRepo.checkSource(Source);

        //        result = response.IsSuccess;
        //        logger.Info("Check Source result " + result);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex);

        //        return ex.ToString();
        //    }

        //}

        public async Task<CertDetails> GetCertDetails(string Source)
        {
            try
            {
                CertDetails response = new CertDetails();

               // response = await _IGenericRepo.GetCertificateDetails(Source);

                return response;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
