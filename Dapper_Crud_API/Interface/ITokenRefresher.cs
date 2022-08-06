using Dapper_Crud_API.Model;

namespace Dapper_Crud_API.Interface
{
    public interface ITokenRefresher
    {
        TokenResponce Refresh(RefreshCred refreshCred);
    }
}
