using EmployeeManagementWebAPI.Models;
using EmployeeManagementWebAPI.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            try
            {
                var employees = await _employeeService.GetAllEmployeesDB();
                if (employees != null)
                {
                    return Ok(employees);
                }
                return NotFound("not found employees");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/Managers
        [HttpGet("managers")]
        public async Task<IActionResult> GetAllManagers()
        {
            try
            {
                var managers = await _employeeService.GetAllManagersDB();
                if (managers != null)
                {
                    return Ok(managers);
                }
                return NotFound("not found managers");
            }
            catch (Exception)
            {
                throw new Exception("Not found Managers");
            }
        }

        // POST api/add_new_employee
        [HttpPost]
        [Route("add_new_employee")]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            var result = await _employeeService.AddEmployeeDB(employee);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Employee not added");

        }
        [HttpPost]
        [Route("edit_employee")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            var result = await _employeeService.EditEmployeeDB(employee);
            if (result != null)
            {
                return Ok(result);
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
            return Ok(result);
        }
    }
}
