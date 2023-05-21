using Authentication.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Application.Services
{
    public class ValidateTokenService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        public ValidateTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        public async Task<ClaimsPrincipal?> ValidateTokenAsync(string token)
        {
            try
            {
                // Create a JWT token handler
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:ScretKey"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                 {
                new Claim(JwtRegisteredClaimNames.Sub, "1234567890"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var jwtToken = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: credentials
                );

                // Create and print the JWT string
                var tokenString = tokenHandler.WriteToken(jwtToken);

                // Validate the JWT string
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey
                };
                var claimsPrincipal = tokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);

                return claimsPrincipal;
            }
            catch (SecurityTokenException)
            {
                // Token validation failed
                return null;
            }
        }
    }
}
