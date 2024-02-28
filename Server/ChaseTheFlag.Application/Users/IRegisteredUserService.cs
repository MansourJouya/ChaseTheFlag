using ChaseTheFlag.Domain.Entities.Users; // Importing necessary namespaces

namespace ChaseTheFlag.Application.Users
{
    /// <summary>
    /// Interface defining operations for registered user service.
    /// </summary>
    public interface IRegisteredUserService
    {
        Task<string> AuthenticateAndGetTokenAsync(string username, string password);
        string GenerateUserSalt();
        string GenerateUserHash(string password, string salt);
        Task<RegisteredUser> GetByIdAsync(int id);
        Task<RegisteredUser> GetByIdLocalAsync(string tag);
        Task<List<RegisteredUser>> GetAllByRoomIDAsync(int roomID);
        Task AddAsync(RegisteredUser user);
        Task UpdateAsync(RegisteredUser user);
        Task DeleteAsync(int id);
    }
}
