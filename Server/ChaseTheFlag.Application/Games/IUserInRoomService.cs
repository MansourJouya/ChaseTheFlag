using ChaseTheFlag.Domain.Entities.Games; // Importing necessary namespaces

namespace ChaseTheFlag.Application.Games
{
    /// <summary>
    /// Interface defining operations for user in room service.
    /// </summary>
    public interface IUserInRoomService
    {
        Task<UserInRoomData> GetByIdAsync(int id);
        Task<List<UserInRoomData>> GetAllAsync();
        Task AddAsync(UserInRoomData userInRoom);
        Task UpdateAsync(UserInRoomData userInRoom);
        Task DeleteAsync(int id);
    }
}
