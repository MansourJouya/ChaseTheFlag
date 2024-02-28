using ChaseTheFlag.Domain.Entities.Chats;

namespace ChaseTheFlag.Infrastructure.Repositories.Chats
{
    /// <summary>
    /// Interface for the repository handling private chat messages.
    /// </summary>
    public interface IChatPrivateRepository
    {
        Task<IEnumerable<ChatPrivate>> GetAllAsync(int userIdSend, int userIdReceive);
        Task AddAsync(ChatPrivate chat);
    }
}
