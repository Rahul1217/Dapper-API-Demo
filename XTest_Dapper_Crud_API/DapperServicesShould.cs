using Dapper_Crud_API.Controllers;
using Dapper_Crud_API.Enum;
using Dapper_Crud_API.Interface;
using Dapper_Crud_API.Model;
using FluentAssertions;
using Moq;
using XTest_Dapper_Crud_API.MockData;
using Xunit;

namespace XTest_Dapper_Crud_API
{
    public class DapperServicesShould
    {
        [Fact]
        public async void GetAll()
        {
            var _service = new Mock<IDapperServices>();
            _service.Setup(x => x.GetAll()).ReturnsAsync(ToDoMockData.GetAll());
            var DapperConObj = new DapperTestController(_service.Object);
            var result = await DapperConObj.Get();

            result.Should().HaveCountGreaterThan(1);
        }
        [Fact]
        public async void GetById()
        {
            var _service = new Mock<IDapperServices>();
            int id = 1;
            _service.Setup(x => x.GetById(id)).ReturnsAsync(ToDoMockData.GetById());
            var DapperConObj = new DapperTestController(_service.Object);
            var result = await DapperConObj.Get(id);

            result.Should().NotBeNull();
        }
        [Fact]
        public void Add()
        {
            var _service = new Mock<IDapperServices>();
            
            _service.Setup(x => x.add(ToDoMockData.Add()));
            var DapperConObj = new DapperTestController(_service.Object);
            DapperConObj.Post(ToDoMockData.Add());
        }

        [Fact]
        public void Update()
        {
            var _service = new Mock<IDapperServices>();
            int Id = 3;
            _service.Setup(x => x.Update(ToDoMockData.Update()));
            var DapperConObj = new DapperTestController(_service.Object);
            DapperConObj.Put(Id, ToDoMockData.Update());
        }

        [Fact]
        public void Delete()
        {
            var _service = new Mock<IDapperServices>();
            int Id = 3;
            _service.Setup(x => x.Delete(Id));
            var DapperConObj = new DapperTestController(_service.Object);
            DapperConObj.Delete(Id);
        }
    }
}