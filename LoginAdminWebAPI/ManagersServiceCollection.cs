using LoginAdminWebAPI.Services.TokenBuilder;
using LoginAdminWebAPI.Services.Providers;
namespace LoginAdminWebAPI
{
    public class ManagersServiceCollection
    {
        private readonly IServiceCollection _services;
        public ManagersServiceCollection(IServiceCollection services)
        {
            _services = services;
            _services.AddScoped<ITokenBuilder, TokenBuilder>();
            _services.AddScoped<IAuthProvider, AuthProvider>();
        }
    }
}
