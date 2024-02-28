using System.Security.Cryptography;
using System.Text;

namespace ChaseTheFlag.Domain.Services
{
    /// <summary>
    /// Provides methods for generating salt and hashing passwords.
    /// </summary>
    public class PasswordHashingService : IPasswordHashingService
    {
        /// <summary>
        /// Generates a random salt as a Base64-encoded string.
        /// </summary>
        /// <returns>The generated salt.</returns>
        public string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Hashes the given password using SHA256 algorithm and provided salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use for hashing.</param>
        /// <returns>The hashed password as a Base64-encoded string.</returns>
        public string HashPassword(string password, string salt)
        {
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] combinedBytes = CombineByteArrays(passwordBytes, saltBytes);
                byte[] hashedBytes = SHA256.HashData(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (log, rethrow, etc.)
                Console.WriteLine($"Error hashing password: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Combines two byte arrays into a single byte array.
        /// </summary>
        /// <param name="array1">The first byte array.</param>
        /// <param name="array2">The second byte array.</param>
        /// <returns>The combined byte array.</returns>
        private static byte[] CombineByteArrays(byte[] array1, byte[] array2)
        {
            byte[] combined = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array1, 0, combined, 0, array1.Length);
            Buffer.BlockCopy(array2, 0, combined, array1.Length, array2.Length);
            return combined;
        }
    }
}
