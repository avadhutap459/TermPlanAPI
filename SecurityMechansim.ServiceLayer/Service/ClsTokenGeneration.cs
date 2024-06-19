using SecurityMechansim.ServiceLayer.Interface;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SecurityMechansim.ServiceLayer.Service
{
    public class ClsTokenGeneration : IDisposable, ITokengeneration
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        bool disposed = false;

        public ClsTokenGeneration()
        {

        }

        ~ClsTokenGeneration()
        {
            Dispose(false);
        }


        public string GenerateToken(string sourcename,string secretkey,string customerid)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var permClaims = new List<Claim>()
                {
                    new Claim("Source", sourcename),
                    new Claim("CustomerId", customerid)
                };
                var tokeOptions = new JwtSecurityToken(
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials);
                string jwt_token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return jwt_token;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public Tuple<string,string> ValidateToken(string token, string secretkey)
        {
            if (token == null)
                return new Tuple<string, string>(string.Empty,string.Empty);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string source = jwtToken.Claims.First(x => x.Type == "Source").Value;
                string customerId = jwtToken.Claims.First(x => x.Type == "CustomerId").Value;

                return new Tuple<string, string>(source, customerId);
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
