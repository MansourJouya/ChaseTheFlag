using System.ComponentModel.DataAnnotations;

namespace ChaseTheFlag.Client.Data.Models
{
    /// <summary>
    /// Represents local user data used for authentication within the ChaseTheFlag client application.
    /// </summary>
    public class UserDataLocal
    {
        /// <summary>
        /// Gets or sets the user's identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's role.
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the authentication token associated with the user.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the user is authenticated.
        /// </summary>
        public bool IsAuthenticated { get; set; }
    }
}
