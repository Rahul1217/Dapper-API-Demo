using Dapper_Crud_API.Interface;
using Dapper_Crud_API.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Dapper_Crud_API.Services
{
    public class JwtToken : IJwtToken
    {
        private IRefreshTokenGenrator _refreshToken;
        private IConfiguration _configuration;
        private readonly IDapperServices dapperRepository;

        public JwtToken(IRefreshTokenGenrator refreshToken, IDapperServices dapperService)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            _configuration = builder.Build();
            dapperRepository = dapperService;
            _refreshToken = refreshToken;
        }

        public TokenResponce Authenticate(string UseiID, Claim[] claims)
        {
            TokenResponce _tokenresponce = new TokenResponce();
            var authsignkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
            var jwtsecuritytoken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: new SigningCredentials(authsignkey, SecurityAlgorithms.HmacSha256)
                );
            var reftoken = _refreshToken.GenrateToken();
            _tokenresponce.JWTToken = new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
            _tokenresponce.RefreshToken = reftoken;

            User_RefreshToken user_RefreshToken = new User_RefreshToken();
            user_RefreshToken.UserID = UseiID;
            user_RefreshToken.RefreshToken = reftoken;

            var refdata = dapperRepository.GetTokenById(UseiID);
            if (refdata.Result != null)
            {
                //UserRefreshToken[UseiID] = reftoken;
                dapperRepository.Update(user_RefreshToken);
            }
            else
            {
                dapperRepository.addRefToken(user_RefreshToken);
            }

            return _tokenresponce;

        }
        public TokenResponce Authenticate(Dapper_Login dapper_Login)
        {
            // Check User is valid Or not user are valid then thos code executed

            TokenResponce _tokenresponce = new TokenResponce();
            var authclaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,dapper_Login.UseiID),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
            var _issuer = _configuration["JWT:ValidIssuer"];
            var authsignkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
            var Token = new JwtSecurityToken(
                issuer: _issuer.ToString(),
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(5),
                claims: authclaim,
                signingCredentials: new SigningCredentials(authsignkey, SecurityAlgorithms.HmacSha256)
                );
            var reftoken = _refreshToken.GenrateToken();
            _tokenresponce.JWTToken = new JwtSecurityTokenHandler().WriteToken(Token);
            _tokenresponce.RefreshToken = reftoken;


            User_RefreshToken user_RefreshToken = new User_RefreshToken();
            user_RefreshToken.UserID = dapper_Login.UseiID;
            user_RefreshToken.RefreshToken = reftoken;

            var refdata = dapperRepository.GetTokenById(dapper_Login.UseiID);
            if (refdata.Result != null)
            {
                dapperRepository.Update(user_RefreshToken);
            }
            else
            {
                dapperRepository.addRefToken(user_RefreshToken);
            }
            return _tokenresponce;
        }
    }
}
