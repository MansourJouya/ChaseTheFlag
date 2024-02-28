using ChaseTheFlag.Domain.Entities.Games;

namespace ChaseTheFlag.Infrastructure.Repositories.Games
{
    /// <summary>
    /// Interface for the repository handling registered rooms.
    /// </summary>
    public interface IRegisteredRoomRepository
    {
        Task<RegisteredRoom> GetByIdAsync(int id);
        Task<ICollection<RegisteredRoom>> GetAllAsync(int id, bool isMyRoom);
        Task AddAsync(RegisteredRoom room);
        Task UpdateAsync(RegisteredRoom room);
        Task DeleteAsync(int id);
    }
}
