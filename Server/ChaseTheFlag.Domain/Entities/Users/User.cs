namespace ChaseTheFlag.Domain.Entities.Users
{
    /// <summary>
    /// Base class representing a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the color associated with the user.
        /// </summary>
        public string Color { get; set; }
    }

    /// <summary>
    /// Class representing user data for registration, inheriting from User.
    /// </summary>
    public class RegisteredUserData : User
    {
        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Class representing a registered user, inheriting from User.
    /// </summary>
    public class RegisteredUser : User
    {
        /// <summary>
        /// Gets or sets the unique identifier of the registered user.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the role of the registered user.
        /// </summary>
        public string UserRole { get; set; } = "Admin";

        /// <summary>
        /// Gets or sets the number of wins achieved by the user.
        /// </summary>
        public int NumberOfWins { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of losses experienced by the user.
        /// </summary>
        public int NumberOfLosses { get; set; } = 0;

        /// <summary>
        /// Gets or sets the player tag associated with the user.
        /// </summary>
        public string PlayerTag { get; set; } = "";

        /// <summary>
        /// Gets or sets the registration date and time of the user.
        /// </summary>
        public DateTime RegistrationDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the salt used for password hashing.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
