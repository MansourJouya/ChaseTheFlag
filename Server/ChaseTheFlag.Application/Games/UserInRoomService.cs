using ChaseTheFlag.Domain.Entities.Games; // Importing necessary namespaces
using ChaseTheFlag.Infrastructure.Repositories.Games;

namespace ChaseTheFlag.Application.Games
{

    /// <summary>
    /// Implementation of the user in room service interface.
    /// </summary>
    public class UserInRoomService : IUserInRoomService
    {
        private readonly IUserInRoomRepository _userInRoomRepository;

        public UserInRoomService(IUserInRoomRepository userInRoomRepository)
        {
            _userInRoomRepository = userInRoomRepository ?? throw new ArgumentNullException(nameof(userInRoomRepository));
        }

        /// <inheritdoc/>
        public async Task<UserInRoomData> GetByIdAsync(int id)
        {
            return await _userInRoomRepository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<List<UserInRoomData>> GetAllAsync()
        {
            return await _userInRoomRepository.GetAllAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(UserInRoomData userInRoom)
        {
            if (userInRoom == null)
            {
                throw new ArgumentNullException(nameof(userInRoom));
            }

            await _userInRoomRepository.AddAsync(userInRoom);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(UserInRoomData userInRoom)
        {
            if (userInRoom == null)
            {
                throw new ArgumentNullException(nameof(userInRoom));
            }

            await _userInRoomRepository.UpdateAsync(userInRoom);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            await _userInRoomRepository.DeleteAsync(id);
        }
    }
}
