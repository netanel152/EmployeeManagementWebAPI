using EmployeeManagementWebAPI.Services.EmployeeServices;

namespace EmployeeManagementWebAPI.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly EmployeeManagementSystemDbContext _dbcontext;

        public AdminService(ILogger<EmployeeService> logger, EmployeeManagementSystemDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }


    }
}
