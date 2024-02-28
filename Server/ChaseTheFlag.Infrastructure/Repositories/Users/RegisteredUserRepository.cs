using ChaseTheFlag.Domain.Entities.Users;
using ChaseTheFlag.Domain.Services;
using ChaseTheFlag.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ChaseTheFlag.Infrastructure.Repositories.Users
{

    /// <summary>
    /// Repository class for handling registered users.
    /// </summary>
    public class RegisteredUserRepository : IRegisteredUserRepository
    {
        private readonly DbContextConnection _context;
        private readonly IUserExtractor _userExtractor;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IJwtTokenService _jwtTokenService;

        public RegisteredUserRepository(DbContextConnection context, IUserExtractor userExtractor, IPasswordHashingService passwordHashingService, IJwtTokenService jwtTokenService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userExtractor = userExtractor;
            _passwordHashingService = passwordHashingService;
            _jwtTokenService = jwtTokenService;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(RegisteredUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<RegisteredUser>> GetAllByRoomIdAsync(int roomId)
        {
            ArgumentNullException.Equals(roomId, 0);
            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            var roomIds = _context.UserInRoomDatas
                          .Where(x => x.RoomId == roomId)
                          .Select(x => x.UserId)
                          .ToList();

            return await _context.RegisteredUsers.Where(r => roomIds.Contains(r.Id)).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<RegisteredUser> GetByIdLocalAsync(string tag)
        {
            return await _context.RegisteredUsers.FirstOrDefaultAsync(x => x.PlayerTag == tag) ?? null!;
        }

        /// <inheritdoc/>
        public async Task<RegisteredUser> GetByIdAsync(int id)
        {
            ArgumentNullException.Equals(id, 0);
            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            return await _context.RegisteredUsers.FindAsync(id) ?? throw new InvalidOperationException($"Registered user entity with the specified ID {id} not found.");
        }

        /// <inheritdoc/>
        public async Task AddAsync(RegisteredUser user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (await _context.RegisteredUsers.AnyAsync(u => u.Username == user.Username))
                throw new InvalidOperationException($"A user with the same username already exists.");

            _context.RegisteredUsers.Add(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<string> AuthenticateAndGetTokenAsync(string username, string password)
        {
            RegisteredUser user = await _context.RegisteredUsers.FirstOrDefaultAsync(u => u.Username == username) ?? throw new InvalidOperationException("Invalid username or password.");
            string hashedPassword = _passwordHashingService.HashPassword(password, user.PasswordSalt);

            if (string.IsNullOrEmpty(hashedPassword) || hashedPassword != user.PasswordHash)
                throw new InvalidOperationException($"Invalid username or password.");

            return _jwtTokenService.GenerateToken(user);
        }

        /// <inheritdoc/>
        public async Task<bool> UserExistsAsync(string username, string nationalCode)
        {
            return await _context.RegisteredUsers.AnyAsync(u => u.Username == username);
        }

        /// <inheritdoc/>
        public string GenerateSalt()
        {
            return _passwordHashingService.GenerateSalt();
        }

        /// <inheritdoc/>
        public string GenerateHash(string password, string salt)
        {
            return _passwordHashingService.HashPassword(password, salt);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var user = await _context.RegisteredUsers.FindAsync(id);
            if (user != null)
            {
                _context.RegisteredUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
