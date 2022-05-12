using Dapper_Crud_API.Enum;
using Dapper_Crud_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest_Dapper_Crud_API.MockData
{
    public class ToDoMockData
    {
        public static List<Dapper_Test> GetAll()
        {
            return new List<Dapper_Test>()
            {
                new Dapper_Test
                {
                    Id=1,
                    Name="Rahul Patel",
                    Status=Status.Active,
                    Description="Test"
                },
                new Dapper_Test
                {
                    Id=1,
                    Name="Sejal Patel",
                    Status=Status.Active,
                    Description="Test"
                }
            };
        }
        public static Dapper_Test GetById()
        {
            return new Dapper_Test
            {
                Id = 1,
                Name = "Rahul Patel",
                Status = Status.Active,
                Description = "Test"
            };
        }
        public static Dapper_Test Add()
        {
            return new Dapper_Test()
            {
                Name = "Rahul Patel",
                Status = Status.Active,
                Description = "Test"
            };
        }
        public static Dapper_Test Update()
        {
            return new Dapper_Test()
            {
                Name = "Rahul Patel",
                Status = Status.Active,
                Description = "Test"
            };
        }
    }
}
