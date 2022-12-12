﻿using LoginAdminWebAPI.Models.Auth;
using LoginAdminWebAPI.Services.Providers;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAdminWebAPI.Services.TokenBuilder
{
    public class TokenBuilder : ITokenBuilder  
    {
        private readonly ILogger<TokenBuilder> _logger;
        private readonly IAuthProvider _authProvider;
        public TokenBuilder(IAuthProvider authProvider, ILogger<TokenBuilder> logger)
        {
            _authProvider = authProvider;
            _logger = logger;
        }
        public async Task<AuthToken> BuildToken(AuthAdmin authAdmin)
        {
            _logger.LogDebug($"TokenBuilder => BuildToken => authUser : {JsonConvert.SerializeObject(authAdmin)}");
            try
            {
                var user = await _authProvider.Login(authAdmin);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_application_api_secret"));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var expirationDate = DateTime.UtcNow.AddMonths(1);

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, authAdmin.Username.ToString()),
                new Claim(ClaimTypes.UserData,authAdmin.Username.ToString()),
                new Claim("AdminId",user.AdminId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var token = new JwtSecurityToken(audience: "searchAudience",
                                                  issuer: "searchIssuer",
                                                  claims: claims,
                                                  expires: expirationDate,
                                                  signingCredentials: credentials);

                var authToken = new AuthToken();
                authToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
                authToken.ExpirationDate = expirationDate;
                authToken.UserName = user.Username;

                return authToken;
            }
            catch (Exception ex)
            {
                _logger.LogError($"TokenBuilder => BuildToken => Message : {ex.Message}");
                throw new Exception("Failed to BuildToken");
            }

        }

        public async Task<ClaimsPrincipal?> GetUserByJWT(HttpContext? httpContext)
        {
            _logger.LogDebug($"TokenBuilder => GetUserByJWT");
            try
            {
                var token = httpContext?.Request.Headers.Authorization;
                var cleanJwt = token.Value.ToString().Split(' ')[1];
                var principal = ValidateToken(cleanJwt);
                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"TokenBuilder => GetUserByJWT => Message : {ex.Message}");
                throw new Exception("Failed to GetUserByJWT");
            }
        }


        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = "searchAudience";
            validationParameters.ValidIssuer = "searchIssuer";
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_application_api_secret"));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}
