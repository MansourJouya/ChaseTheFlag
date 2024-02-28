using ChaseTheFlag.Domain.Entities.Games;

namespace ChaseTheFlag.Infrastructure.Repositories.Games
{
    /// <summary>
    /// Interface for the repository handling users in a room.
    /// </summary>
    public interface IUserInRoomRepository
    {
        Task<UserInRoomData> GetByIdAsync(int id);
        Task<List<UserInRoomData>> GetAllAsync();
        Task AddAsync(UserInRoomData userInRoom);
        Task UpdateAsync(UserInRoomData userInRoom);
        Task DeleteAsync(int id);
    }
}
