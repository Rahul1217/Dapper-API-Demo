using Dapper_Crud_API.Interface;
using Dapper_Crud_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dapper_Crud_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DapperTestController : ControllerBase
    {
        private readonly IDapperServices dapperRepository;
       

        public DapperTestController(IDapperServices dapperService)
        {
            dapperRepository = dapperService;
        }

        [HttpGet]
        public async Task<IEnumerable<Dapper_Test>> Get()
        {
            return await dapperRepository.GetAll();
        }

        [HttpGet("{id}")]
        //public async Task<Dapper_Test> Get([FromHeader(Name = "x-CustomHeader2")]  int id)
        public async Task<Dapper_Test> Get(int id)
        {
            return await dapperRepository.GetById(id);
        }
        [HttpPost]
        public void Post([FromBody] Dapper_Test dapper_Test)
        {
            if (ModelState.IsValid)
                dapperRepository.add(dapper_Test);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Dapper_Test dapper_Test)
        {
            dapper_Test.Id = id;
            if (ModelState.IsValid)
                dapperRepository.Update(dapper_Test);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dapperRepository.Delete(id);
        }

        
    }
}
