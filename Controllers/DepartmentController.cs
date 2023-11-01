using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController<Department, APIdbContext>
    {
        public DepartmentController(APIdbContext context) : base(context) { }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
        {
            return await GetAllEntities<Department>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            return await GetEntityById<Department>(id);
        }
        [HttpPut]
        public async Task<IActionResult> PutDepartment(Department departmentModel)
        {
            return await UpdateEntity(departmentModel);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department departmentModel)
        {
            return await CreateEntity(departmentModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            return await DeleteEntity<Department>(id);
        }

    }
}
