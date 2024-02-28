
namespace ChaseTheFlag.Client.Data.Links
{
    /// <summary>
    /// Static class to manage API endpoints and secret keys for the ChaseTheFlag client application.
    /// </summary>
    public static class ApiEndpoints
    {
        /// <summary>
        /// Gets the base URL of the API.
        /// </summary>
        /// <returns>The base URL of the API.</returns>
        public static string GetApiBaseUrl() => "https://localhost:7037/api/";

        /// <summary>
        /// Gets the main URL of the SignalR hub.
        /// </summary>
        /// <returns>The main URL of the SignalR hub.</returns>
        public static string SignalRHubUrl() => "https://localhost:7037/signalhub";

        /// <summary>
        /// The secret key used for authentication or other purposes.
        /// </summary>
        public const string SecretKey = "fp7nTBpBqS5UCitNN9ChV5edVCA0DCEZmt8MbPjqOaEDAsinwAFcxDDhkrDeFhFO";

        // Define your encryption key
        public static string EncryptionKey = "vjnsfrwdpuhbqotlkm";
    }
}
