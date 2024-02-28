using ChaseTheFlag.Domain.Entities.Users; // Importing necessary namespaces
using ChaseTheFlag.Infrastructure.Repositories.Users;

namespace ChaseTheFlag.Application.Users
{

    /// <summary>
    /// Implementation of the registered user service interface.
    /// </summary>
    public class RegisteredUserService : IRegisteredUserService
    {
        private readonly IRegisteredUserRepository _registeredUserRepository;

        public RegisteredUserService(IRegisteredUserRepository registeredUserRepository)
        {
            _registeredUserRepository = registeredUserRepository ?? throw new ArgumentNullException(nameof(registeredUserRepository));
        }

        /// <inheritdoc/>
        public async Task<List<RegisteredUser>> GetAllByRoomIDAsync(int roomId)
        {
            return await _registeredUserRepository.GetAllByRoomIdAsync(roomId);
        }

        /// <inheritdoc/>
        public Task<string> AuthenticateAndGetTokenAsync(string username, string password)
        {
            return _registeredUserRepository.AuthenticateAndGetTokenAsync(username, password);
        }

        /// <inheritdoc/>
        public string GenerateUserSalt()
        {
            return _registeredUserRepository.GenerateSalt();
        }

        /// <inheritdoc/>
        public string GenerateUserHash(string password, string salt)
        {
            return _registeredUserRepository.GenerateHash(password, salt);
        }

        /// <inheritdoc/>
        public async Task AddAsync(RegisteredUser user)
        {
            await _registeredUserRepository.AddAsync(user);
        }

        /// <inheritdoc/>
        public async Task<RegisteredUser> GetByIdAsync(int id)
        {
            return await _registeredUserRepository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<RegisteredUser> GetByIdLocalAsync(string tag)
        {
            return await _registeredUserRepository.GetByIdLocalAsync(tag);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(RegisteredUser user)
        {
            await _registeredUserRepository.UpdateAsync(user);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            await _registeredUserRepository.DeleteAsync(id);
        }
    }
}
