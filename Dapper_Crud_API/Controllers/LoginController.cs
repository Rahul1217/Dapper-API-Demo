using Dapper_Crud_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dapper_Crud_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
         private IConfiguration _configuration;

        public LoginController()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            _configuration = builder.Build();
        }

        [HttpPost]
        [Route("Token")]
        public IActionResult gettoken([FromBody] Dapper_Login dapper_Login)
        {

            if (!string.IsNullOrEmpty(dapper_Login.UseiID))
            {
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
                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(Token)
                });
            }
            return Unauthorized();

        }
    }
}
