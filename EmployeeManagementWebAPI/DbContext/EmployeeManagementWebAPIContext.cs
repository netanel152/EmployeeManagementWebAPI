using EmployeeManagementWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementWebAPI.dbContext
{
    public partial class EmployeeManagementWebAPIContext : DbContext
    {
        public EmployeeManagementWebAPIContext()
        {

        }

        public EmployeeManagementWebAPIContext(DbContextOptions<EmployeeManagementWebAPIContext> options)
             : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }= null!;
    }
}
