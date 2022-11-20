using EmployeeManagementWebAPI.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesDB();
            if (employees != null)
            {
                return Ok(employees);
            }
            return NotFound("not found employees");
        }

        // GET: api/Managers
        [HttpGet("managers")]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await _employeeService.GetAllManagersDB();
            if (managers != null)
            {
                return Ok(managers);
            }
            return NotFound("not found managers");
        }

        // POST api/add_new_employee
        [HttpPost("add_new_employee")]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            var result = await _employeeService.AddEmployeeDB(employee);
            if (result != null)
            {
                return Ok("Employee is Added");
            }
            return BadRequest("Employee is not added");
        }

        [HttpPost("edit_employee")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            var result = await _employeeService.EditEmployeeDB(employee);
            if (result != null)
            {
                return Ok("Employee is updated");
            }
            return BadRequest("Employee is not updated");
        }

        // POST api/Employees/5
        [HttpDelete("delete_employee" + "/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var result = await _employeeService.DeleteEmployeeDB(id);
            if (result is null)
            {
                return NotFound("Employee not found.");
            }
            return Ok("Employee is Deleted");
        }
    }
}
