using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChaseTheFlag.Infrastructure.Middlewares
{
    /// <summary>
    /// Middleware for validating JWT tokens in incoming requests.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="JwtTokenValidationMiddleware"/> class.
    /// </remarks>
    /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
    public class JwtTokenValidationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

        /// <summary>
        /// Invokes the middleware to validate JWT tokens in incoming requests.
        /// </summary>
        /// <param name="context">The HttpContext for the current request.</param>
        public async Task Invoke(HttpContext context)
        {
            var userIdClaim = context.User.FindFirst("U");

            //context.Request.Path.Value.StartsWith("/api/SignalR/sendMessage") ||

            // Allow requests to authentication and registration endpoints without token validation
            if (context.Request.Path.Value.StartsWith("/api/RegisteredUsers/authenticate") ||
                context.Request.Path.Value.StartsWith("/api/RegisteredUsers/registry") ||
                  context.Request.Path.Value.StartsWith("/signalhub"))
            {
                await _next(context);
                return;
            }

            // Validate user and management IDs
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid user or management ID.");
                return;
            }

            // Check token expiration
            var expireDateClaim = context.User.FindFirst(ClaimTypes.Expiration);
            if (expireDateClaim != null && DateTime.TryParse(expireDateClaim.Value, out var expireDate))
            {
                if (expireDate < DateTime.UtcNow)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Token has expired.");
                    return;
                }
            }

            // Store user and management IDs in HttpContext items for downstream middleware
            context.Items["UserId"] = userId;

            await _next(context);
        }
    }
}
