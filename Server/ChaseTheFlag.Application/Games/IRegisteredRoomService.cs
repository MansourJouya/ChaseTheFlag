using ChaseTheFlag.Domain.Entities.Games; // Importing necessary namespaces

namespace ChaseTheFlag.Application.Games
{
    /// <summary>
    /// Interface defining operations for registered room service.
    /// </summary>
    public interface IRegisteredRoomService
    {
        Task<RegisteredRoom> GetByIdAsync(int id);
        Task<ICollection<RegisteredRoom>> GetAllAsync(int id, bool isMyRoom);
        Task AddAsync(RegisteredRoom room);
        Task UpdateAsync(RegisteredRoom room);
        Task DeleteAsync(int id);
    }
}
