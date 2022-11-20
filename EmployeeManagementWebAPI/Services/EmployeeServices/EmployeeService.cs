using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmployeeManagementWebAPI.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly EmployeeManagementSystemDbContext _dbcontext;
        public EmployeeService(EmployeeManagementSystemDbContext dbcontext, ILogger<EmployeeService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<List<Employee>> GetAllEmployeesDB()
        {
            _logger.LogDebug($"EmployeeService => GetAllEmployeesDB");
            try
            {
                List<Employee> employees = await _dbcontext.Employees.ToListAsync();
                if (employees is null)
                {
                    return null;
                }
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmployeeService => GetAllEmployeesDB => Message : {ex.Message}");
                throw new Exception("No employees in the database");
            }
        }

        public async Task<List<Employee>> GetAllManagersDB()
        {
            _logger.LogDebug($"EmployeeService => GetAllManagersDB");

            try
            {
                var managers = await _dbcontext.Employees.Where(s => !string.IsNullOrEmpty(s.ManagerName)).ToListAsync();
                if (managers is null)
                {
                    return null;
                }
                return managers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmployeeService => GetAllManagersDB => Message : {ex.Message}");
                throw new Exception("No managers in the database");
            }

        }

        public async Task<Employee> AddEmployeeDB(Employee employee)
        {
            _logger.LogDebug($"EmployeeService => AddEmployeeDB => {JsonConvert.SerializeObject(employee)} employee : {employee}");
            try
            {
                if (employee == null)
                {
                    return null;
                }
                await _dbcontext.Employees.AddAsync(employee);
                await _dbcontext.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmployeeService => AddEmployeeDB => Message : {ex.Message}");
                throw new Exception("Error, Employee not created");
            }
        }



        public async Task<Employee> EditEmployeeDB(Employee employee)
        {
            _logger.LogDebug($"EmployeeService => EditEmployeeDB => {JsonConvert.SerializeObject(employee)} employee : {employee}");
            try
            {
                var employeeToEdit = _dbcontext.Employees.FirstOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
                if (employeeToEdit == null)
                {
                    return null;
                }

                employeeToEdit.EmployeeName = employee.EmployeeName;
                employeeToEdit.EmployeeRole = employee.EmployeeRole;
                employeeToEdit.ManagerName = employee.ManagerName;

                _dbcontext.Entry(employeeToEdit).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

                return employeeToEdit;
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmployeeService => EditEmployeeDB => Message : {ex.Message}");
                throw new Exception("Error, Employee not edited");
            }
        }

        public async Task<Employee> DeleteEmployeeDB(string id)
        {
            _logger.LogDebug($"EmployeeService => DeleteEmployeeDB => {JsonConvert.SerializeObject(id)} id : {id}");
            try
            {
                if (_dbcontext.Employees == null)
                {
                    return null;
                }
                var employee = await _dbcontext.Employees.FindAsync(id);
                if (employee == null)
                {
                    return null;
                }
                _dbcontext.Entry(employee).State = EntityState.Deleted;
                await _dbcontext.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmployeeService => DeleteEmployeeDB => Message : {ex.Message}");
                throw new Exception("Error, Employee not deleted");
            }
        }
    }
}
