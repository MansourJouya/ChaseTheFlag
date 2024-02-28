using ChaseTheFlag.Domain.Entities.Chats;
using ChaseTheFlag.Infrastructure.Context;
using ChaseTheFlag.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace ChaseTheFlag.Infrastructure.Repositories.Chats
{

    /// <summary>
    /// Repository class for handling private chat messages.
    /// </summary>
    public class ChatPrivateRepository : IChatPrivateRepository
    {
        private readonly DbContextConnection _context;
        private readonly IUserExtractor _userExtractor;

        public ChatPrivateRepository(DbContextConnection context, IUserExtractor userExtractor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userExtractor = userExtractor ?? throw new ArgumentNullException(nameof(userExtractor));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ChatPrivate>> GetAllAsync(int userIdSend, int userIdReceive)
        {
            if (userIdSend == 0 || userIdReceive == 0)
                throw new ArgumentException("User ID cannot be 0.");

            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            return await _context.ChatPrivates
                .Where(x => (x.UserIdSend == userIdSend && x.UserIdReceive == userIdReceive) ||
                            (x.UserIdSend == userIdReceive && x.UserIdReceive == userIdSend))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(ChatPrivate chat)
        {
            ArgumentNullException.ThrowIfNull(chat);

            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            _context.ChatPrivates.Add(chat);
            await _context.SaveChangesAsync();
        }
    }
}
