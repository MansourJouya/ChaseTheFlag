using ChaseTheFlag.Domain.Entities.Chats;
using ChaseTheFlag.Infrastructure.Context;
using ChaseTheFlag.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace ChaseTheFlag.Infrastructure.Repositories.Chats
{

    /// <summary>
    /// Repository class for handling public chat messages.
    /// </summary>
    public class ChatPublicRepository : IChatPublicRepository
    {
        private readonly DbContextConnection _context;
        private readonly IUserExtractor _userExtractor;

        public ChatPublicRepository(DbContextConnection context, IUserExtractor userExtractor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userExtractor = userExtractor ?? throw new ArgumentNullException(nameof(userExtractor));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ChatPublic>> GetAllAsync()
        {
            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            return await _context.ChatPublic.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(ChatPublic chat)
        {
            ArgumentNullException.ThrowIfNull(chat);

            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            _context.ChatPublic.Add(chat);
            await _context.SaveChangesAsync();
        }
    }
}
