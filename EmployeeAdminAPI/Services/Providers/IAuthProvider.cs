using LoginAdminWebAPI.Models.Admin;
using LoginAdminWebAPI.Models.Auth;

namespace LoginAdminWebAPI.Services.Providers
{
    public interface IAuthProvider
    {
        public Task<Admin> Login(AuthAdmin authAdmin);
        public bool CheckAuthentication();
    }
}
