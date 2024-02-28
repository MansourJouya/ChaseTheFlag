namespace ChaseTheFlag.Infrastructure.Repositories.Users
{
    /// <summary>
    /// Result of user extraction indicating whether the user exists and the user ID.
    /// </summary>
    public class UserExtractionResult
    {
        public bool UserExists { get; }
        public int UserId { get; }

        public UserExtractionResult(bool userExists, int userId)
        {
            UserExists = userExists;
            UserId = userId;
        }
    }
}
