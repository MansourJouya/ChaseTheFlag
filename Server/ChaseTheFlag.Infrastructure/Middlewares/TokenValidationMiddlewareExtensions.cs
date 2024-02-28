using Microsoft.AspNetCore.Builder;

namespace ChaseTheFlag.Infrastructure.Middlewares
{
    /// <summary>
    /// Extension methods for adding token validation middleware to the application pipeline.
    /// </summary>
    public static class TokenValidationMiddlewareExtensions
    {
        /// <summary>
        /// Adds token validation middleware to the application pipeline.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseTokenValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtTokenValidationMiddleware>();
        }
    }
}
