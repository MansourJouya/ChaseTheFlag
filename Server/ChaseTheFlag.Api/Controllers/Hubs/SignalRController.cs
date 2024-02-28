using ChaseTheFlag.Application.Hubs.Serviec;
using Microsoft.AspNetCore.Mvc;

namespace ChaseTheFlag.Api.Controllers.Hubs
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignalRController : ControllerBase
    {
        private readonly HubService _signalRService;

        public SignalRController(HubService signalRService)
        {
            _signalRService = signalRService;
        }


        [HttpPost("sendMessage/{playerTag}/{connectionId}")]
        public async Task<IActionResult> SendMessageAsync(string playerTag, string connectionId)
        {
            await _signalRService.UpdateUserConnectionAsync(playerTag, connectionId);
            return Ok("Message sent successfully.");
        }





        [HttpPost("StatusUserInGroup/{roomID}")]
        public async Task<IActionResult> StatusUserInGroupAsync(int roomID)
        {
            await _signalRService.SendStatusMessageToGroupAysnc(roomID);
            return Ok("Message sent successfully.");
        }

        [HttpPost("RemoveUserInGroup/{PlayerTag}")]
        public async Task<IActionResult> RemoveUserInGroupAsync(string PlayerTag)
        {
            await _signalRService.RemoveUserInGroupAsync(PlayerTag);
            return Ok("Message sent successfully.");
        }




        [HttpPost("StartGame")]
        public async Task<IActionResult> StartGameAsync([FromBody] byte[] comamnd)
        {
            await _signalRService.StartGameAsync(comamnd);
            return Ok("OK");
        }


        [HttpPost("UpdateGame")]
        public async Task<IActionResult> UpdateGameAsync([FromBody] byte[] comamnd)
        {
            await _signalRService.UpdateGameAsync(comamnd);
            return Ok("OK");
        }






        [HttpPost("AddUserInGroup/{playerTag}/{roodId}")]
        public async Task<IActionResult> AddUserInGroupAsync(string playerTag, int roodId)
        {
            await _signalRService.AddUserInGroupAsync(playerTag, roodId);
            return Ok("Message sent successfully.");
        }



    }
}
