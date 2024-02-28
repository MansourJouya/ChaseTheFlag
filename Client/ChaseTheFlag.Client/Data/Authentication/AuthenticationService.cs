using ChaseTheFlag.Client.Data.Authentication.Additions;
using ChaseTheFlag.Client.Data.Links;
using ChaseTheFlag.Client.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChaseTheFlag.Client.Data.Authentication
{
    /// <summary>
    /// This class manages authentication-related operations.
    /// </summary>
    public class AuthenticationService(HttpClient httpClient, JwtSecurityTokenHandler tokenHandler, AuthenticationStateProvider authenticationStateProvider)
    {
        private readonly HttpClient _httpClient = httpClient;  // HTTP client for making requests
        private readonly JwtSecurityTokenHandler _tokenHandler = tokenHandler;  // Handler for JWT tokens
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));  // Provider for authentication state

        /// <summary>
        /// Signs out the user.
        /// </summary>
        public async Task Logout()
        {
            await UpdateAuthenticationDataAsync(null);
        }

        /// <summary>
        /// Retrieves user account information asynchronously.
        /// </summary>
        public async Task<Result<UserDataLocal?>> RetrieveUserAccountAsync()
        {
            try
            {
                // Get the custom authentication state provider
                var customAuthStateProvider = _authenticationStateProvider as CustomAuthenticationStateProvider;
                // Retrieve the authentication state
                var authenticationState = await customAuthStateProvider!.GetAuthenticationStateAsync();
                // Check if user is authenticated
                if (authenticationState?.User?.Identity?.IsAuthenticated != true)
                    return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");

                // Retrieve user ID and role from claims
                string userId = authenticationState.User.FindFirst("U")?.Value ?? string.Empty;
                string userRole = authenticationState.User.FindFirst("R")?.Value ?? string.Empty;

                // Create a new User object with retrieved information
                var userAccount = new UserDataLocal
                {
                    IsAuthenticated = true,
                    Id = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0,
                    Role = userRole
                };

                // Generate a token for the user
                userAccount.Token = GenerateToken(userAccount);

                // Check if user account is valid
                if (IsValidUserAccount(userAccount))
                    return Result<UserDataLocal?>.Success(userAccount);

                return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");
            }
            catch
            {
                return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");
            }
        }

        /// <summary>
        /// Retrieves user information asynchronously by username and password.
        /// </summary>
        public async Task<Result<UserDataLocal?>> GetUserAsync(string apiUrl)
        {
            try
            {
                // Make a request to authenticate the user
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Read the token from the response
                    string token = await response.Content.ReadAsStringAsync();

                    // Check if the credentials are valid
                    if (token == "Invalid username or password")
                        return Result<UserDataLocal?>.Fail("Invalid username or password.");
                    else
                    {
                        // Generate a user account from the token
                        var userGenerated = CreateUserAccountFromToken(token);
                        // Update authentication data
                        var update = await UpdateAuthenticationDataAsync(userGenerated.Value!);
                        // Check if the update was successful
                        if (update.Status == ResultStatus.Success)
                            return userGenerated;
                        else
                            return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");
                    }
                }
                else
                {
                    // Update authentication data if request fails
                    await UpdateAuthenticationDataAsync(null);
                    return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");
                }
            }
            catch
            {
                // Update authentication data if an error occurs
                await UpdateAuthenticationDataAsync(null);
                return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");
            }
        }

        /// <summary>
        /// Generates a user account from a JWT token.
        /// </summary>
        private Result<UserDataLocal?> CreateUserAccountFromToken(string jwtToken)
        {
            try
            {
                if (_tokenHandler.ReadToken(jwtToken) is JwtSecurityToken jsonToken)
                {
                    // Retrieve user ID and role from the token claims
                    var userId = RetrieveClaimValue(jsonToken, "U");
                    var userRole = RetrieveClaimValue(jsonToken, "R");

                    if (int.TryParse(userId, out int parsedUserId))
                    {
                        // Create a new User object with retrieved information
                        var userAccount = new UserDataLocal
                        {
                            Id = parsedUserId,
                            Role = userRole,
                            Token = jwtToken,
                            IsAuthenticated = true
                        };

                        return Result<UserDataLocal?>.Success(userAccount);
                    }
                }
                return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");
            }
            catch
            {
                return Result<UserDataLocal?>.Fail("An error occurred. Please try again.");
            }
        }

        /// <summary>
        /// Updates authentication data asynchronously.
        /// </summary>
        private async Task<Result<bool>> UpdateAuthenticationDataAsync(UserDataLocal? userValue)
        {
            // Check if the authentication state provider is of the correct type
            if (_authenticationStateProvider is not CustomAuthenticationStateProvider customAuthStateProvider)
                return Result<bool>.Fail("Authentication state provider is not of the correct type.");

            // Update authentication data
            await customAuthStateProvider.UpdateAuthAsync(userValue!);
            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Generates a JWT token for a user.
        /// </summary>
        private static string GenerateToken(UserDataLocal userAccount)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiEndpoints.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                int expiresMinutes = 200;

                var claims = new List<Claim>
                {
                    new Claim("U", userAccount.Id.ToString() ?? ""),
                    new Claim("R", userAccount.Role ?? ""),
                };

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                    signingCredentials: credentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenString;
            }
            catch
            {
                return null!;
            }
        }

        /// <summary>
        /// Checks if a user account is valid.
        /// </summary>
        private static bool IsValidUserAccount(UserDataLocal userAccount)
        {
            return userAccount.IsAuthenticated &&
                   !string.IsNullOrEmpty(userAccount.Role) &&
                   !string.IsNullOrEmpty(userAccount.Token);
        }

        /// <summary>
        /// Retrieves the value of a claim from a JWT token.
        /// </summary>
        private static string RetrieveClaimValue(JwtSecurityToken jsonToken, string claimType)
        {
            return jsonToken.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value!;
        }
    }
}
