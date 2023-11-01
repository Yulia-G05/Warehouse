using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee, APIdbContext>

    {
        public EmployeeController(APIdbContext context) : base(context) { }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            return await GetAllEntities<Employee>();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            return await GetEntityById<Employee>(id);
        }
        [HttpPut]
        public async Task<IActionResult> PutEmployee(Employee employeeModel)
        {
            return await UpdateEntity(employeeModel);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employeeModel)
        {
            return await CreateEntity(employeeModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            return await DeleteEntity<Employee>(id);
        }
    }
}
