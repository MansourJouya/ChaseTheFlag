using ChaseTheFlag.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChaseTheFlag.Infrastructure.Repositories.Users
{

    /// <summary>
    /// Service for extracting user information.
    /// </summary>
    public class UserExtractor : IUserExtractor
    {
        private readonly IMemoryCache _cache;
        private readonly DbContextConnection _dbContext;
        private readonly HttpContext _httpContext;

        public UserExtractor(IHttpContextAccessor httpContextAccessor, IMemoryCache cache, DbContextConnection dbContext)
        {
            _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public async Task<UserExtractionResult> ExtractUser()
        {
            try
            {
                int userId = 0;

                // Attempt to retrieve user ID from HTTP context items
                if (_httpContext.Items.TryGetValue("UserId", out var userIdObject) &&
                    (userIdObject is int userIdValue))
                {
                    userId = userIdValue;
                }

                // If user ID is not found or is zero, return user extraction result indicating user does not exist
                if (userId == 0)
                    return new UserExtractionResult(false, 0);

                var cacheKey = $"{userId}";

                // Check if user existence is cached
                if (!_cache.TryGetValue(cacheKey, out bool userExists))
                {
                    // If not cached, query the database to check user existence
                    userExists = await _dbContext.RegisteredUsers.AnyAsync(u => u.Id == userId);

                    // If user does not exist in the database, return user extraction result indicating user does not exist
                    if (!userExists)
                        return new UserExtractionResult(false, 0);

                    // Cache the user existence for future requests
                    _cache.Set(cacheKey, true);
                }

                // Return user extraction result indicating user exists
                return new UserExtractionResult(true, userId);
            }
            catch
            {
                // In case of any exceptions, return user extraction result indicating user does not exist
                return new UserExtractionResult(false, 0);
            }
        }
    }
}
