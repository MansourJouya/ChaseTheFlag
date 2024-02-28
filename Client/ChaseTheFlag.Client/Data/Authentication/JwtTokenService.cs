using ChaseTheFlag.Client.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ChaseTheFlag.Client.Data.Authentication
{
    /// <summary>
    /// Service responsible for generating JWT tokens for user authentication.
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <param name="secretKey">The secret key used for token generation.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateJwtToken(UserDataLocal user)
        {
            // Convert the SecretKey to a byte array for SymmetricSecurityKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Links.ApiEndpoints.SecretKey));

            // Create signing credentials using the key and HmacSha256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create a SecurityTokenDescriptor with subject, expiration time, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim("U", user.Id.ToString() ?? ""),
                new Claim("R", user.Role ?? ""),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = credentials
            };

            // Create a JwtSecurityTokenHandler to generate the token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create a JWT token based on the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Write the token as a string
            var tokenString = tokenHandler.WriteToken(token);

            // Return the generated JWT token string
            return tokenString;

        }
    }
}
