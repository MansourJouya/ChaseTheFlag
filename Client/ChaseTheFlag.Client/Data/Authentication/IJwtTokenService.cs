using ChaseTheFlag.Client.Data.Models;

namespace ChaseTheFlag.Client.Data.Authentication
{
    /// <summary>
    /// Interface for JWT token service.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>The generated JWT token.</returns>
        string GenerateJwtToken(UserDataLocal user);
    }
}
