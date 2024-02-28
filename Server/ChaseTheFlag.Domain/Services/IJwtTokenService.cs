using ChaseTheFlag.Domain.Entities.Users;

namespace ChaseTheFlag.Domain.Services
{
    /// <summary>
    /// Interface for a service responsible for generating JWT tokens.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified registered user.
        /// </summary>
        /// <param name="user">The registered user for whom the token is generated.</param>
        /// <returns>A JWT token string.</returns>
        string GenerateToken(RegisteredUser user);
    }
}
