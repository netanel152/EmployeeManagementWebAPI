namespace EmployeeManagementWebAPI.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetAllManagersDB();
        public Task<List<Employee>> GetAllEmployeesDB();
        public Task<Employee?> AddEmployeeDB(Employee employee);
        public Task<Employee?> EditEmployeeDB(Employee employee);
        public Task<Employee?> DeleteEmployeeDB(string id);
    }
}
