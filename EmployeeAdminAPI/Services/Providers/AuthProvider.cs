using LoginAdminWebAPI.Models.Admin;
using LoginAdminWebAPI.Models.Auth;
using LoginAdminWebAPI.Services.Extentions;

namespace LoginAdminWebAPI.Services.Providers
{
    public class AuthProvider : IAuthProvider
    {
        private readonly ILogger<AuthProvider> _logger;
        private readonly LoginAdminContext _loginAdminContext;
        public AuthProvider(LoginAdminContext loginAdminContext, ILogger<AuthProvider> logger)
        {
            _loginAdminContext = loginAdminContext;
            _logger = logger;
        }

        public bool CheckAuthentication()
        {
            throw new NotImplementedException();
        }

        public async Task<Admin?> Login(AuthAdmin authAdmin)
        {
            _logger.LogDebug($"AuthProvider => Login => authUser : {authAdmin}");
            try
            {
                var admin = _loginAdminContext.Admins.GetUserAndCheckAuthentication(authAdmin).FirstOrDefault();
                if (admin == null)
                {
                    throw new Exception();
                }
                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AuthProvider => Login => Message : {ex.Message}");
                throw new Exception("Failed to Login");
            }
        }
    }

}
