using LoginAdminWebAPI.Models.Admin;
using Microsoft.EntityFrameworkCore;

namespace LoginAdminWebAPI
{
    public class LoginAdminContext : DbContext
    {
        public LoginAdminContext(DbContextOptions<LoginAdminContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }

    }
}
