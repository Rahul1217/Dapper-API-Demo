using Dapper_Crud_API.Model;
using System.Security.Claims;

namespace Dapper_Crud_API.Interface
{
    public interface IJwtToken
    {
        TokenResponce Authenticate(string UseiID, Claim[] claims);
        TokenResponce Authenticate(Dapper_Login dapper_Login);
    }
}
