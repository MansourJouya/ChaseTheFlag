namespace ChaseTheFlag.Domain.Services
{
    /// <summary>
    /// Interface for a service responsible for password hashing.
    /// </summary>
    public interface IPasswordHashingService
    {
        /// <summary>
        /// Generates a random salt for password hashing.
        /// </summary>
        /// <returns>A string representing the generated salt.</returns>
        string GenerateSalt();

        /// <summary>
        /// Hashes the specified password using the given salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt used for hashing.</param>
        /// <returns>A string representing the hashed password.</returns>
        string HashPassword(string password, string salt);
    }
}
