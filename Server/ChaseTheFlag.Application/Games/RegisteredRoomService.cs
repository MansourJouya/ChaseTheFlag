using ChaseTheFlag.Domain.Entities.Games; // Importing necessary namespaces
using ChaseTheFlag.Infrastructure.Repositories.Games;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChaseTheFlag.Application.Games
{

    /// <summary>
    /// Implementation of the registered room service interface.
    /// </summary>
    public class RegisteredRoomService : IRegisteredRoomService
    {
        private readonly IRegisteredRoomRepository _registeredRoomRepository;

        public RegisteredRoomService(IRegisteredRoomRepository registeredRoomRepository)
        {
            _registeredRoomRepository = registeredRoomRepository ?? throw new ArgumentNullException(nameof(registeredRoomRepository));
        }

        /// <inheritdoc/>
        public async Task<RegisteredRoom> GetByIdAsync(int id)
        {
            return await _registeredRoomRepository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<ICollection<RegisteredRoom>> GetAllAsync(int id, bool isMyRoom)
        {
            return await _registeredRoomRepository.GetAllAsync(id, isMyRoom);
        }

        /// <inheritdoc/>
        public async Task AddAsync(RegisteredRoom room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            await _registeredRoomRepository.AddAsync(room);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(RegisteredRoom room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            await _registeredRoomRepository.UpdateAsync(room);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            await _registeredRoomRepository.DeleteAsync(id);
        }
    }
}
