using ChaseTheFlag.Domain.Entities.Chats;

namespace ChaseTheFlag.Infrastructure.Repositories.Chats
{
    /// <summary>
    /// Interface for the repository handling public chat messages.
    /// </summary>
    public interface IChatPublicRepository
    {
        Task<IEnumerable<ChatPublic>> GetAllAsync();
        Task AddAsync(ChatPublic chat);
    }
}
