
using Blazored.SessionStorage;
using ChaseTheFlag.Client.Data.Models;
using System.Security.Claims;

namespace ChaseTheFlag.Client.Data.Authentication
{
    /// <summary>
    /// Custom authentication state provider for managing user authentication state.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CustomAuthenticationStateProvider"/> class.
    /// </remarks>
    /// <param name="sessionStorage">The session storage service.</param>
    public class CustomAuthenticationStateProvider(ISessionStorageService sessionStorage) : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));

        /// <summary>
        /// Updates the authentication state asynchronously.
        /// </summary>
        /// <param name="userAccount">The user account data to update.</param>
        public async Task UpdateAuthAsync(UserDataLocal userAccount)
        {
            try
            {
                if (userAccount != null)
                {
                    // Encrypt the user account data and store it in session storage
                    await _sessionStorage.SetItemAsync("UserAccount", EncryptionHelper.Encrypt(userAccount));

                    // Create a claims identity based on user account data
                    var claimsIdentity = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("U", userAccount.Id.ToString() ?? ""),
                        new Claim("R", userAccount.Role ?? ""),
                    });

                    // Create a claims principal based on the claims identity
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Notify authentication state change with the new claims principal
                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
                }
                else
                {
                    // Remove user account data from session storage
                    await _sessionStorage.RemoveItemAsync("UserAccount");

                    // Create an empty claims principal
                    var claimsPrincipal = new ClaimsPrincipal();

                    // Notify authentication state change with an empty claims principal
                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
                }
            }
            catch
            {
                // In case of an error, remove user account data from session storage
                await _sessionStorage.RemoveItemAsync("UserAccount");

                // Create an empty claims principal
                var claimsPrincipal = new ClaimsPrincipal();

                // Notify authentication state change with an empty claims principal
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }

        }

        /// <summary>
        /// Retrieves the current authentication state asynchronously.
        /// </summary>
        /// <returns>The current authentication state.</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Retrieve user account data from session storage
            var stringAccount = await _sessionStorage.GetItemAsync<string>("UserAccount");

            // Initialize a claims identity
            ClaimsIdentity identity;

            // Check if user account data exists
            if (stringAccount != null)
            {
                // Decrypt the user account data
                var userAccount = EncryptionHelper.Decrypt(stringAccount);

                // Create a claims identity based on the decrypted user account data
                identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("U", userAccount.Id.ToString() ?? ""),
                    new Claim("R", userAccount.Role ?? ""),
                }, "jwt");
            }
            else
            {
                // Create an empty claims identity
                identity = new ClaimsIdentity();
            }

            // Create a claims principal based on the claims identity
            var user = new ClaimsPrincipal(identity);

            // Create an authentication state with the user's claims principal
            var state = new AuthenticationState(user);

            // Notify authentication state change with the new authentication state
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            // Return the authentication state
            return state;

        }
    }
}


//using Blazored.SessionStorage;
//using ChaseTheFlag.Client.Models;
//using SimpleEncryption;
//using System.Security.Claims;
//using System.Text.Json;

//namespace ChaseTheFlag.Client.Authentication
//{
//    public class CustomAuthenticationStateProvider(ISessionStorageService sessionStorage) : AuthenticationStateProvider
//    {

//        private readonly ISessionStorageService _sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));

//        public async Task UpdateAuthAsync(UserDataLocal userAccount)
//        {
//            try
//            {
//                if (userAccount != null)
//                {
//                    await _sessionStorage.SetItemAsync("UserAccount", EncryptionHelper.Encrypt(userAccount));
//                    var claimsIdentity = new ClaimsIdentity(new List<Claim>
//                    {
//                        new Claim("U", userAccount.Id.ToString() ?? ""),
//                        new Claim("R", userAccount.Role ?? ""),
//                    });
//                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
//                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
//                }
//                else
//                {
//                    await _sessionStorage.RemoveItemAsync("UserAccount");
//                    var claimsPrincipal = new ClaimsPrincipal();
//                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
//                }
//            }
//            catch
//            {
//                await _sessionStorage.RemoveItemAsync("UserAccount");
//                var claimsPrincipal = new ClaimsPrincipal();
//                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
//            }
//        }

//        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//        {
//            var stringAccount = await _sessionStorage.GetItemAsync<string>("UserAccount");
//            ClaimsIdentity identity;
//            if (stringAccount != null)
//            {
//                var userAccount = EncryptionHelper.Decrypt(stringAccount);
//                identity = new ClaimsIdentity(new List<Claim>
//                {
//                    new Claim("U", userAccount.Id.ToString() ?? ""),
//                    new Claim("R", userAccount.Role ?? ""),
//                }, "jwt");
//            }
//            else
//            {
//                identity = new ClaimsIdentity();
//            }

//            var user = new ClaimsPrincipal(identity);
//            var state = new AuthenticationState(user);
//            NotifyAuthenticationStateChanged(Task.FromResult(state));
//            return state;
//        }


//        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
//        {
//            var payload = jwt?.Split('.')[1];
//            if (payload == null)
//                return Enumerable.Empty<Claim>();

//            var jsonBytes = ParseBase64WithoutPadding(payload);
//            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
//            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString()));
//        }
//        private static byte[] ParseBase64WithoutPadding(string base64)
//        {
//            switch (base64.Length % 4)
//            {
//                case 2: base64 += "=="; break;
//                case 3: base64 += "="; break;
//            }
//            return Convert.FromBase64String(base64);
//        }
//    }
//}

