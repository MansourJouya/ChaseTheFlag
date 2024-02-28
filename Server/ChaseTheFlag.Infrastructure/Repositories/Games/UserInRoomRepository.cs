using ChaseTheFlag.Domain.Entities.Games;
using ChaseTheFlag.Infrastructure.Context;
using ChaseTheFlag.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace ChaseTheFlag.Infrastructure.Repositories.Games
{

    /// <summary>
    /// Repository class for handling users in a room.
    /// </summary>
    public class UserInRoomRepository : IUserInRoomRepository
    {
        private readonly DbContextConnection _context;
        private readonly IUserExtractor _userExtractor;

        public UserInRoomRepository(DbContextConnection context, IUserExtractor userExtractor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userExtractor = userExtractor ?? throw new ArgumentNullException(nameof(userExtractor));
        }

        /// <inheritdoc/>
        public async Task AddAsync(UserInRoomData userInRoom)
        {
            ArgumentNullException.ThrowIfNull(userInRoom);

            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            if (await _context.UserInRoomDatas.AnyAsync(u => u.RoomId == userInRoom.RoomId && u.UserId == userInRoom.UserId))
                throw new InvalidOperationException("User is already a member of this room.");

            _context.UserInRoomDatas.Add(userInRoom);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<UserInRoomData> GetByIdAsync(int id)
        {
            return await _context.UserInRoomDatas.FindAsync(id) ?? throw new InvalidOperationException($"User in room entity with the specified ID {id} not found.");
        }

        /// <inheritdoc/>
        public async Task<List<UserInRoomData>> GetAllAsync()
        {
            return await _context.UserInRoomDatas.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(UserInRoomData userInRoom)
        {
            _context.Entry(userInRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var userInRoom = await _context.UserInRoomDatas.FindAsync(id);
            if (userInRoom != null)
            {
                _context.UserInRoomDatas.Remove(userInRoom);
                await _context.SaveChangesAsync();
            }
        }
    }
}
