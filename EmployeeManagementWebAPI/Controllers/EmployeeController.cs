using EmployeeManagementWebAPI.dbContext;
using EmployeeManagementWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeManagementWebAPIContext _context;

        public EmployeeController(EmployeeManagementWebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            try
            {
                if (_context.Employees == null)
                {
                    return NotFound();
                }
                return await _context.Employees.ToListAsync();
            }
            catch (Exception)
            {

                throw new Exception("Not found Employees");
            }

        }

        // GET: api/Managers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllManagers(string id)
        {
            try
            {
                var managers = await _context.Employees.Where(s => s.EmployeeId == id && s.ManagerName != null).ToListAsync();
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
        public async Task<Employee> GetManagerById(string id)
        {
            return await _context.Employees.Where(s => s.EmployeeId == id).SingleOrDefaultAsync();
        }

        // POST api/add_employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return NotFound();
                }
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
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
            _context.Entry(employee).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);    
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
