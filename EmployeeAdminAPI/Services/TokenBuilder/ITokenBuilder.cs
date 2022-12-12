using LoginAdminWebAPI.Models.Auth;
using System.Security.Claims;

namespace LoginAdminWebAPI.Services.TokenBuilder
{
    public interface ITokenBuilder
    {
        public Task<AuthToken> BuildToken(AuthAdmin authUser);
        public Task<ClaimsPrincipal?> GetUserByJWT(HttpContext? httpContext);
    }
}
