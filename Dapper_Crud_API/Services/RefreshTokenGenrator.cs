using Dapper_Crud_API.Interface;
using System.Security.Cryptography;

namespace Dapper_Crud_API.Services
{
    public class RefreshTokenGenrator : IRefreshTokenGenrator
    {
        public string GenrateToken()
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenrator=RandomNumberGenerator.Create())
            {
                randomnumbergenrator.GetBytes(randomnumber);
                return Convert.ToBase64String(randomnumber);
            }
        }
    }
}
