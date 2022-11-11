using EmployeeManagementWebAPI.Models;
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
        [Route("Employees"), HttpGet]
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
        [Route("Managers")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllManagers(string id)
        {
            try
            {
                var managers = await _dbcontext.Employees.Where(s => s.EmployeeId == id && s.ManagerName != null).ToListAsync();
                if (managers == null)
                {
                    return NotFound();
                }
                return managers;
            }
            catch (Exception)
            {

                throw new Exception("Not found Managers");
            }

        }

        // GET api/Employees/5
        [HttpGet("{id}")]
        public async Task<Employee> GetManagerById(string id) => await _dbcontext.Employees.Where(s => s.EmployeeId == id).SingleOrDefaultAsync();

        // POST api/add_new_employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
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

        // PUT api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee(string id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
            _dbcontext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }

        // DELETE api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
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
            _dbcontext.Employees.Remove(employee);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return (_dbcontext.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
