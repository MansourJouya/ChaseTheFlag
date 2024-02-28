using ChaseTheFlag.Client.Data.Links;
using ChaseTheFlag.Client.Data.Models;
using System.Text;
using System.Text.Json;

namespace ChaseTheFlag.Client.Data.Authentication
{
    /// <summary>
    /// Provides methods for encrypting and decrypting UserDataLocal objects.
    /// </summary>
    public static class EncryptionHelper
    {


        /// <summary>
        /// Encrypts a UserDataLocal object.
        /// </summary>
        /// <param name="user">The UserDataLocal object to encrypt.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(UserDataLocal user)
        {
            // Convert User object to JSON string
            string jsonData = JsonSerializer.Serialize(user);

            // Encrypt the JSON data using the encryption key
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(jsonData + ApiEndpoints.EncryptionKey);
            string encryptedData = Convert.ToBase64String(encryptedBytes);

            return encryptedData;
        }

        /// <summary>
        /// Decrypts an encrypted string into a UserDataLocal object.
        /// </summary>
        /// <param name="encryptedData">The encrypted string to decrypt.</param>
        /// <returns>The decrypted UserDataLocal object.</returns>
        public static UserDataLocal Decrypt(string encryptedData)
        {
            // Decrypt the encrypted data
            byte[] decryptedBytes = Convert.FromBase64String(encryptedData);
            string decryptedData = Encoding.UTF8.GetString(decryptedBytes);

            // Remove encryption key from decrypted data
            string jsonData = decryptedData.Replace(ApiEndpoints.EncryptionKey, "");

            // Convert JSON string back to UserDataLocal object
            UserDataLocal user = JsonSerializer.Deserialize<UserDataLocal>(jsonData)!;

            return user!;
        }
    }
}
