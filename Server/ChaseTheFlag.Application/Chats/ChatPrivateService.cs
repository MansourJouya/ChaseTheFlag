using ChaseTheFlag.Application.Hubs.Serviec; // Importing necessary namespaces
using ChaseTheFlag.Domain.Entities.Chats;
using ChaseTheFlag.Infrastructure.Repositories.Chats;

namespace ChaseTheFlag.Application.Chats
{

    /// <summary>
    /// Implementation of the private chat service interface.
    /// </summary>
    public class ChatPrivateService : IChatPrivateService
    {
        private readonly IChatPrivateRepository _chatPrivateRepository;
        private readonly HubService _signalRService;

        public ChatPrivateService(IChatPrivateRepository chatPrivateRepository, HubService signalRService)
        {
            _chatPrivateRepository = chatPrivateRepository ?? throw new ArgumentNullException(nameof(chatPrivateRepository));
            _signalRService = signalRService ?? throw new ArgumentNullException(nameof(signalRService));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ChatPrivate>> GetAllChatsAsync(int userIdSend, int userIdReceive)
        {
            return await _chatPrivateRepository.GetAllAsync(userIdSend, userIdReceive);
        }

        /// <inheritdoc/>
        public async Task AddChatAsync(ChatPrivate chat)
        {
            // Send the chat message via SignalR
            await _signalRService.SendMessagePrivateAsync(chat.PlayerTag, chat);

            // Add the chat message to the repository
            await _chatPrivateRepository.AddAsync(chat);
        }
    }
}
