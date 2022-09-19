using Dapper_Crud_API.Interface;
using Dapper_Crud_API.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dapper_Crud_API.Services
{
    public class TokenRefresher : ITokenRefresher
    {
        private IConfiguration _configuration;
        private IJwtToken _jwtToken;
        private readonly IDapperServices dapperRepository;
        public TokenRefresher(IJwtToken jwtToken, IDapperServices dapperService)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            _configuration = builder.Build();
            _jwtToken = jwtToken;
            dapperRepository = dapperService;
        }
        public TokenResponce Refresh(RefreshCred refreshCred)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken ValidatedToken;
            var pricipal = tokenHandler.ValidateToken(refreshCred.JWTToken,
                new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]))

                }, out ValidatedToken);

            var jwttoken = ValidatedToken as JwtSecurityToken;
            if (jwttoken == null || !jwttoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                throw new SecurityTokenException("Invalid Token Pass!");
            }

            string? username = pricipal.Identity != null ? pricipal.Identity.Name : "";
            var refdata = dapperRepository.GetTokenById((username != null ? username : ""));

            if (refdata.Result == null || refreshCred.RefreshToken != refdata.Result.RefreshToken)
            {
                throw new SecurityTokenException("Invalid Token Pass!");
            }
            return _jwtToken.Authenticate((username != null ? username : ""), pricipal.Claims.ToArray());
        }
    }
}
