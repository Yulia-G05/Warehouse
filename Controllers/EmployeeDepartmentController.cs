using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDepartmentController : BaseController<EmployeeDeportament, APIdbContext>
    {
        public EmployeeDepartmentController(APIdbContext context) : base(context)
        {
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllDepartmentEmployees()
        {
            if (_context.EmployeeDeportaments == null)
            {
                return NotFound();
            }

            var departmentEmployees = await _context.EmployeeDeportaments.ToListAsync();
            var employeeInfoList = new List<object>();

            foreach (var departmentEmployee in departmentEmployees)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == departmentEmployee.DepartmentID);
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == departmentEmployee.EmployeeID);

                if (department != null && employee != null)
                {
                    var employeeDeportmant = new
                    {
                        EmployeeId = departmentEmployee.EmployeeID,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DepartmentID = department.DepartmentID,
                        DepartmentName = department.DepartmenName
                    };

                    employeeInfoList.Add(employeeDeportmant);
                }
            }

            if (employeeInfoList.Count == 0)
            {
                return NotFound();
            }

            return Ok(employeeInfoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDeportament>> GetDepartmentEmployee(int id)
        {
            if (_context.EmployeeDeportaments == null)
            {
                return NotFound();
            }
            var emloyeedeport = await _context.EmployeeDeportaments.FindAsync(id);
            if (emloyeedeport == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == emloyeedeport.DepartmentID);
            if (department == null)
            {
                return NotFound();
            }
            var emplyee = await _context.Employees.FirstOrDefaultAsync(b => b.EmployeeID == emloyeedeport.EmployeeID);
            if (emplyee == null)
            {
                return NotFound();
            }
            var employeeDeportmant = new
            {
                EmployeeId = emloyeedeport.EmployeeID,
                FirstName = emplyee.FirstName,
                LastName = emplyee.LastName,
                DepartmentID = department.DepartmentID,
                DepartmentName=department.DepartmenName



            };
            return Ok(employeeDeportmant);
        }
        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDeportament employeeDeportmentModel)
        {
            int id = employeeDeportmentModel.DepartmentEmployeeID;

            var employeeDeportment = await _context.EmployeeDeportaments.FindAsync(id);

            if (employeeDeportment == null)
            {
                return NotFound();
            }


            employeeDeportment.DepartmentEmployeeID = employeeDeportmentModel.DepartmentEmployeeID;
            employeeDeportment.EmployeeID = employeeDeportmentModel.EmployeeID;
            employeeDeportment.DepartmentID = employeeDeportmentModel.DepartmentID;

                _context.Entry(employeeDeportment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
           

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDeportament>> PostDepartmentEmployee(EmployeeDeportament departmentEmployee)
        {
            _context.EmployeeDeportaments.Add(departmentEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentEmployee", new { id = departmentEmployee.DepartmentEmployeeID }, departmentEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDeportament(int id)
        {
            return await DeleteEntity<EmployeeDeportament>(id);
        }
    }
}
