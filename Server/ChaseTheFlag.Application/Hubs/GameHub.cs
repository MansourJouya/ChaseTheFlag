using ChaseTheFlag.Application.Hubs.Serviec;
using Microsoft.AspNetCore.SignalR;

namespace ChaseTheFlag.Application.Hubs
{
    /// <summary>
    /// SignalR hub for real-time communication.
    /// </summary>
    public class GameHub : Hub
    {
        private readonly HubService _signalRService;

        /// <summary>
        /// Constructor for SignalR hub.
        /// </summary>
        /// <param name="signalRService">Service for SignalR operations.</param>
        public GameHub(HubService signalRService)
        {
            _signalRService = signalRService;
        }

        /// <summary>
        /// Method invoked when a connection is disconnected.
        /// </summary>
        /// <param name="exception">Exception that caused the disconnection.</param>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            await _signalRService.RemoveUserAsync(Context.ConnectionId);
        }
    }
}
