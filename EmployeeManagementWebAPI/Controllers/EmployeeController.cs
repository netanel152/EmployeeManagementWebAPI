using EmployeeManagementWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeManagementSystemDbContext _dbcontext;

        public EmployeeController(EmployeeManagementSystemDbContext context)
        {
            _dbcontext = context;
        }

        // GET: api/Employees
        [HttpGet]
        [Route("employees")]
        public async Task<ActionResult> GetAllEmployees()
        {

            try
            {
                List<Employee> employees = await _dbcontext.Employees.ToListAsync();
                if (employees != null)
                {
                    return Ok(employees);
                }
                return Ok("not found employees");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/Managers
        [HttpGet]
        [Route("managers")]
        public async Task<ActionResult> GetAllManagers()
        {
            try
            {
                List<Employee> managers = await _dbcontext.Employees.Where(s => !String.IsNullOrEmpty(s.ManagerName)).ToListAsync();
                if (managers != null)
                {
                    return Ok(managers);
                }
                return Ok("not found managers");
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
            try
            {
                
                if (employee == null)
                {
                    return NotFound();
                }
                await _dbcontext.Employees.AddAsync(employee);
                await _dbcontext.SaveChangesAsync();
                return CreatedAtAction(nameof(AddEmployee), new { id = employee.EmployeeId }, employee);

            }
            catch (Exception)
            {
                throw new Exception("Error, Employee not created");
            }

        }
        [HttpPost]
        [Route("edit_employee")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            try
            {
                Employee employeeToEdit = _dbcontext.Employees.FirstOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
                if (employeeToEdit == null)
                {
                    return BadRequest();
                }

                employeeToEdit.EmployeeName = employee.EmployeeName;
                employeeToEdit.EmployeeRole = employee.EmployeeRole;
                employeeToEdit.ManagerName = employee.ManagerName;

                _dbcontext.Entry(employeeToEdit).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

                return Ok(employeeToEdit);
            }
            catch (Exception)
            {

                throw new Exception("Error, Employee not edited");
            }
        }

        // POST api/Employees/5
        [HttpPost]
        [Route("delete_employee" + "/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                if (_dbcontext.Employees == null)
                {
                    return NotFound();
                }
                var employee = await _dbcontext.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                _dbcontext.Entry(employee).State = EntityState.Deleted;
                await _dbcontext.SaveChangesAsync();

                return Ok("employee deleted");
            }
            catch (Exception)
            {
                throw new Exception("Error, Employee not deleted");
            }
        }
    }
}
