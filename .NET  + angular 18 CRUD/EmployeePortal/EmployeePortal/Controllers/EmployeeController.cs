using EmployeePortal.Models;
using EmployeePortal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository emp;
        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.emp = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> employeeList()
        {
            var employees = await emp.GetAllEmployees();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(Employee em)
        {
            await emp.CreateEmployee(em);
            return Ok(em);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateEmployee(int id, Employee em)
        {
            await emp.UpdateEmployee(id, em);
            return Ok(em);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteEmployee(int id)
        {
            await emp.DeleteEmployee(id);
            return Ok(id);
        }
    }
}
