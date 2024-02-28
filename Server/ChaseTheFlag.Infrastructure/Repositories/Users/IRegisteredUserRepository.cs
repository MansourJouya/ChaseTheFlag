using ChaseTheFlag.Domain.Entities.Users;

namespace ChaseTheFlag.Infrastructure.Repositories.Users
{
    /// <summary>
    /// Interface for the repository handling registered users.
    /// </summary>
    public interface IRegisteredUserRepository
    {
        Task<RegisteredUser> GetByIdAsync(int id);
        Task<RegisteredUser> GetByIdLocalAsync(string tag);
        Task<List<RegisteredUser>> GetAllByRoomIdAsync(int roomId);
        Task AddAsync(RegisteredUser user);
        Task UpdateAsync(RegisteredUser user);
        Task DeleteAsync(int id);
        Task<string> AuthenticateAndGetTokenAsync(string username, string password);
        Task<bool> UserExistsAsync(string username, string nationalCode);
        string GenerateSalt();
        string GenerateHash(string password, string salt);
    }
}
