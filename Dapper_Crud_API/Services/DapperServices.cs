using Dapper;
using Dapper_Crud_API.Interface;
using Dapper_Crud_API.Model;
using Dapper_Crud_API.Repository;
using System.Data;
using System.Reflection;

namespace Dapper_Crud_API.Services
{
    public class DapperServices : Dapper_Repository, IDapperServices
    {
        public DapperServices(IConfiguration configuration)
            : base(configuration)
        { }
        public async void add(Dapper_Test dapper_Test)
        {
            using (var dbconnection = CreateConnection())
            {
                string sQuery = "INSERT INTO [dbo].[tblDapper_Test] (Name, Description, Status, DueDate, CreatedDate, DateModified) VALUES(@Name, @Description, @Status, @DueDate, @CreatedDate, @DateModified)";
                dbconnection.Open();
                await dbconnection.ExecuteAsync(sQuery, dapper_Test);
            }
        }

        public async Task<IEnumerable<Dapper_Test>> GetAll()
        {
            using (var dbconnection = CreateConnection())
            {
                string sQuery = "SELECT Id,Name, Description, Status, DueDate, CreatedDate, DateModified FROM [dbo].[tblDapper_Test]";
                dbconnection.Open();
                return await dbconnection.QueryAsync<Dapper_Test>(sQuery);
            }
        }
        public async Task<Dapper_Test> GetById(int id)
        {
            using (var dbconnection = CreateConnection())
            {
                dbconnection.Open();
                //string sQuery = "SELECT Id,Name, Description, Status, DueDate, CreatedDate, DateModified FROM [dbo].[tblDapper_Test] WHERE Id=@id";
                //var dapperData = await dbconnection.QueryAsync<Dapper_Test>(sQuery, new { Id = id });
                string SP = "[dbo].[SP_Select_By_ID]";
                var dapperData = await Task.FromResult(dbconnection.Query<Dapper_Test>(SP, this.DapperSelectParam(id), commandType: CommandType.StoredProcedure).ToList());

                return dapperData.FirstOrDefault();
            }
        }

        public async void Delete(int id)
        {
            using (var dbconnection = CreateConnection())
            {
                string sQuery = "DELETE FROM [dbo].[tblDapper_Test] WHERE Id=@id";
                dbconnection.Open();
                await dbconnection.ExecuteAsync(sQuery, new { Id = id });
            }
        }

        public async void Update(Dapper_Test dappertest)
        {
            using (var dbconnection = CreateConnection())
            {
                dbconnection.Open();
                //string sQuery = "UPDATE [dbo].[tblDapper_Test] SET Name=@Name, Description=@Description, Status=@Status  WHERE Id=@id";
                //await dbconnection.QueryAsync(sQuery, dappertest);


                string SP = "[dbo].[SP_Update]";
                var dapperData1 = await Task.FromResult(dbconnection.Query<User_RefreshToken>(SP, this.DapperTestParam(dappertest), commandType: CommandType.StoredProcedure).ToList());

            }
        }


        public async void addRefToken(User_RefreshToken user_RefreshToken)
        {
            using (var dbconnection = CreateConnection())
            {
                string sQuery = "INSERT INTO [dbo].[User_RefreshToken] (UserID, RefreshToken) VALUES(@UserID, @RefreshToken)";
                dbconnection.Open();
                await dbconnection.ExecuteAsync(sQuery, user_RefreshToken);
            }
        }
        public async Task<User_RefreshToken> GetTokenById(string userid)
        {
            using (var dbconnection = CreateConnection())
            {
                string sQuery = "SELECT UserID, RefreshToken FROM [dbo].[User_RefreshToken] WHERE UserID=@userid";
                dbconnection.Open();
                var dapperData = await dbconnection.QueryAsync<User_RefreshToken>(sQuery, new { UserID = userid });
                return dapperData.FirstOrDefault();

            }
        }
        public async void Update(User_RefreshToken user_RefreshToken)
        {
            using (var dbconnection = CreateConnection())
            {
                string sQuery = "UPDATE [dbo].[User_RefreshToken] SET RefreshToken=@RefreshToken  WHERE UserID=@UserID";
                dbconnection.Open();
                await dbconnection.QueryAsync(sQuery, user_RefreshToken); ;
            }
        }

        private DynamicParameters DapperTestParam(Dapper_Test dapper_Test)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", dapper_Test.Id);
            dynamicParameters.Add("@Name", dapper_Test.Name);
            dynamicParameters.Add("@Status", dapper_Test.Status);
            dynamicParameters.Add("@DueDate", dapper_Test.DueDate);
            dynamicParameters.Add("@Description", dapper_Test.Description);
            dynamicParameters.Add("@DateModified", dapper_Test.DateModified);
            dynamicParameters.Add("@CreatedDate", dapper_Test.CreatedDate);

            //PropertyInfo[] propInfos = typeof(Dapper_Test).GetProperties();
            //propInfos.ToList().ForEach(p =>
            //    Console.WriteLine(string.Format("Property name: {0}", p.Name))
            //    );


            return dynamicParameters;
        }
        //public static object GetPropValue(object src, string propName = "")
        //{
        //    if (src != null)
        //        return src.GetType().GetProperty(propName).GetValue(src, null);
        //    else
        //        return new object();
        //}
        private DynamicParameters DapperSelectParam(Int64 Id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", Id);
            //dynamicParameters.Add("@Name", dapper_Test.Name);
            //dynamicParameters.Add("@Status", dapper_Test.Status);
            //dynamicParameters.Add("@DueDate", dapper_Test.DueDate);
            //dynamicParameters.Add("@Description", dapper_Test.Description);
            //dynamicParameters.Add("@DateModified", dapper_Test.DateModified);
            //dynamicParameters.Add("@CreatedDate", dapper_Test.CreatedDate);

            return dynamicParameters;
        }

    }
}
