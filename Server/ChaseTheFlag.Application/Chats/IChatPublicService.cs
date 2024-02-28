using ChaseTheFlag.Domain.Entities.Chats;

namespace ChaseTheFlag.Application.Chats
{
    /// <summary>
    /// Interface defining operations for public chat service.
    /// </summary>
    public interface IChatPublicService
    {
        Task<IEnumerable<ChatPublic>> GetAllChatsAsync();
        Task AddChatAsync(ChatPublic chat);
    }
}
