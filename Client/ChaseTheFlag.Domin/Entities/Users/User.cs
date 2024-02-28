using System.ComponentModel.DataAnnotations;

namespace ChaseTheFlag.Domain.Entities.Users
{
    /// <summary>
    /// Base class representing a user.
    /// </summary>
    public class User
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot be more than 100 characters.")]
        [MinLength(3, ErrorMessage = "First name cannot be less than 3 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters.")]
        [MinLength(3, ErrorMessage = "Last name cannot be less than 3 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 50 characters.")]
        [RegularExpression("^(?=.*[a-zA-Z])[a-zA-Z0-9_]{5,}$", ErrorMessage = "Username must contain English letters.")]
        public string Username { get; set; }

        /// <summary>
        /// Default color for a user.
        /// </summary>
        public string Color { get; set; } = "#F44336";
    }

    /// <summary>
    /// Class representing user data for registration.
    /// </summary>
    public class RegisteredUserData : User
    {
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password cannot be less than 8 characters.")]
        [StringLength(50, ErrorMessage = "Password cannot be more than 50 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Password must contain at least 1 letter & 1 digit.")]
        public string Password { get; set; }
    }

    /// <summary>
    /// Class representing a registered user, inheriting from User.
    /// </summary>
    public class RegisteredUser : User
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// User role, default is Admin.
        /// </summary>
        public string UserRole { get; set; } = "Admin";

        /// <summary>
        /// Number of wins.
        /// </summary>
        public int NumberOfWins { get; set; } = 0;

        /// <summary>
        /// Number of losses.
        /// </summary>
        public int NumberOfLosses { get; set; } = 0;

        /// <summary>
        /// Player tag.
        /// </summary>
        public string PlayerTag { get; set; } = "";

        /// <summary>
        /// Registration date and time.
        /// </summary>
        public DateTime RegistrationDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Salt for password hashing.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Hashed password.
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
