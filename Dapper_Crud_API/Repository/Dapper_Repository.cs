using Dapper;
using Dapper_Crud_API.Model;
using System.Data;
using System.Data.SqlClient;

namespace Dapper_Crud_API.Repository
{
    public abstract class Dapper_Repository
    {
        private readonly IConfiguration _configuration;

        protected Dapper_Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
