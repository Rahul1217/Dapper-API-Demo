using Dapper_Crud_API.Model;

namespace Dapper_Crud_API.Interface
{
    public interface IDapperServices
    {
        void add(Dapper_Test dapper_Test);
        Task<IEnumerable<Dapper_Test>> GetAll();
        Task<Dapper_Test> GetById(int id);
        void Delete(int id);
        void Update(Dapper_Test dappertest);
        void addRefToken(User_RefreshToken user_RefreshToken);
        Task<User_RefreshToken> GetTokenById(string userid);
        void Update(User_RefreshToken user_RefreshToken);

    }
}
