namespace EmployeeManagementWebAPI.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }    
        public string? EmployeeName { get; set; }    
        public string? EmployeeRole { get; set; }
        public string? ManagerName { get; set; }
    }
}
