using Dapper_Crud_API.Interface;
using Dapper_Crud_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Dapper_Crud_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IJwtToken _jwtToken;
        private readonly ITokenRefresher _tokenRefresher;
        

        public LoginController(IJwtToken jwtToken, ITokenRefresher tokenRefresher)
        {
            _jwtToken = jwtToken;
            _tokenRefresher = tokenRefresher;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Token")]
        public IActionResult gettoken([FromBody] Dapper_Login dapper_Login)
        {

            if (!string.IsNullOrEmpty(dapper_Login.UseiID))
            {
                var _tokenresponce = _jwtToken.Authenticate(dapper_Login);
                if (_tokenresponce == null)
                    return Unauthorized();

                return Ok(_tokenresponce);
            }
            return Unauthorized();

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult refreshToken([FromBody] RefreshCred refreshCred)
        {
            var Token = _tokenRefresher.Refresh(refreshCred);
            
            if (Token == null)
                return Unauthorized();

            return Ok(Token);
        }
    }
}
