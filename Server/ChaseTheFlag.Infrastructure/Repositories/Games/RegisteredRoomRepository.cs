using ChaseTheFlag.Domain.Entities.Games;
using ChaseTheFlag.Infrastructure.Context;
using ChaseTheFlag.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace ChaseTheFlag.Infrastructure.Repositories.Games
{

    /// <summary>
    /// Repository class for handling registered rooms.
    /// </summary>
    public class RegisteredRoomRepository : IRegisteredRoomRepository
    {
        private readonly DbContextConnection _context;
        private readonly IUserExtractor _userExtractor;

        public RegisteredRoomRepository(DbContextConnection context, IUserExtractor userExtractor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userExtractor = userExtractor ?? throw new ArgumentNullException(nameof(userExtractor));
        }

        /// <inheritdoc/>
        public async Task<RegisteredRoom> GetByIdAsync(int id)
        {
            return await _context.RegisteredRooms.FindAsync(id) ?? throw new InvalidOperationException($"Registered room entity with the specified ID {id} not found.");
        }

        /// <inheritdoc/>
        public async Task<ICollection<RegisteredRoom>> GetAllAsync(int id, bool isMyRoom)
        {
            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            var roomIds = _context.UserInRoomDatas
                .Where(x => x.UserId == id)
                .Select(x => x.RoomId)
                .ToList();

            IQueryable<RegisteredRoom> filteredRoomsQuery = isMyRoom ?
                _context.RegisteredRooms.Where(r => roomIds.Contains(r.Id)) :
                _context.RegisteredRooms.Where(r => !roomIds.Contains(r.Id));

            var rooms = await filteredRoomsQuery.ToListAsync();
            return rooms;
        }

        /// <inheritdoc/>
        public async Task AddAsync(RegisteredRoom room)
        {
            var result = await _userExtractor.ExtractUser();
            if (!result.UserExists)
                throw new InvalidOperationException("User does not exist in the database.");

            if (await _context.RegisteredRooms.AnyAsync(u => u.Name == room.Name))
                throw new InvalidOperationException($"A room with the same name already exists.");

            _context.RegisteredRooms.Add(room);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(RegisteredRoom room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var room = await _context.RegisteredRooms.FindAsync(id);
            if (room != null)
            {
                _context.RegisteredRooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
