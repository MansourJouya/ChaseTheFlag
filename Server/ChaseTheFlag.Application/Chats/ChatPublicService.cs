using ChaseTheFlag.Application.Hubs.Serviec; // Importing necessary namespaces
using ChaseTheFlag.Domain.Entities.Chats;
using ChaseTheFlag.Infrastructure.Repositories.Chats;

namespace ChaseTheFlag.Application.Chats
{

    /// <summary>
    /// Implementation of the public chat service interface.
    /// </summary>
    public class ChatPublicService : IChatPublicService
    {
        private readonly IChatPublicRepository _chatPublicRepository;
        private readonly HubService _signalRService;

        public ChatPublicService(IChatPublicRepository chatPublicRepository, HubService signalRService)
        {
            _chatPublicRepository = chatPublicRepository ?? throw new ArgumentNullException(nameof(chatPublicRepository));
            _signalRService = signalRService ?? throw new ArgumentNullException(nameof(signalRService));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ChatPublic>> GetAllChatsAsync()
        {
            return await _chatPublicRepository.GetAllAsync();
        }

        /// <inheritdoc/>
        public async Task AddChatAsync(ChatPublic message)
        {
            // Send the public chat message via SignalR
            await _signalRService.SendMessagePublicAsync(message);

            // Add the public chat message to the repository
            await _chatPublicRepository.AddAsync(message);
        }
    }
}
