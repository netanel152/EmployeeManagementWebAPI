using LoginAdminWebAPI.Models.Auth;
using LoginAdminWebAPI.Services.TokenBuilder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginAdminWebAPI.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenBuilder _tokenBuilder;
        public AuthController(ITokenBuilder tokenBuilder)
        {
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthAdmin authAdmin)
        {
            var authToken = await _tokenBuilder.BuildToken(authAdmin);
            return Ok(authToken);
        }

        [Authorize]
        [HttpGet("CheckAuth")]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok("If You See It You Are Authorized");
        }


    }
}
