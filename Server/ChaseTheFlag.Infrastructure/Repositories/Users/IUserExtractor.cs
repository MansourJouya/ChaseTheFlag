namespace ChaseTheFlag.Infrastructure.Repositories.Users
{
    /// <summary>
    /// Interface for extracting user information.
    /// </summary>
    public interface IUserExtractor
    {
        /// <summary>
        /// Extracts user information.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the user extraction result.</returns>
        Task<UserExtractionResult> ExtractUser();
    }
}
