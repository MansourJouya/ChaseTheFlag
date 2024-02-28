using ChaseTheFlag.Domain.Entities.Chats;

namespace ChaseTheFlag.Application.Chats
{
    /// <summary>
    /// Interface defining operations for private chat service.
    /// </summary>
    public interface IChatPrivateService
    {
        Task<IEnumerable<ChatPrivate>> GetAllChatsAsync(int userIdSend, int userIdReceive);
        Task AddChatAsync(ChatPrivate chat);
    }
}
