using LoginAdminWebAPI.Models.Admin;
using LoginAdminWebAPI.Models.Auth;

namespace LoginAdminWebAPI.Services.Extentions
{
    public static class AuthFilterExtensions
    {
        public static IQueryable<Admin> GetUserAndCheckAuthentication(this IQueryable<Admin> query, AuthAdmin authAdmin)
        {
            return query.Where(w => w.Username == authAdmin.Username && w.Password == authAdmin.Password);
        }
    }
}
