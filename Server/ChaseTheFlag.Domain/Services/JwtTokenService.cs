using ChaseTheFlag.Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChaseTheFlag.Domain.Services
{
    /// <summary>
    /// Service for generating JWT tokens.
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        private const string SecretKey = "fp7nTBpBqS5UCitNN9ChV5edVCA0DCEZmt8MbPjqOaEDAsinwAFcxDDhkrDeFhFO";

        /// <summary>
        /// Generates a JWT token for the specified registered user.
        /// </summary>
        /// <param name="user">The registered user for whom the token is generated.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateToken(RegisteredUser user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Set expiration time based on your application's requirements
            var expirationMinutes = 100;
            var expirationTime = DateTime.UtcNow.AddMinutes(expirationMinutes);

            var claims = new[]
            {
                new Claim("U", user.Id.ToString() ?? ""),
                new Claim("R", user.UserRole ?? ""),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
