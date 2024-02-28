

using ChaseTheFlag.Application.Chats;
using ChaseTheFlag.Domain.Entities.Chats;
using Microsoft.AspNetCore.Mvc;

namespace ChaseTheFlag.Api.Controllers.Chats
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatPrivateController : ControllerBase
    {
        private readonly IChatPrivateService _chatPrivateService;

        public ChatPrivateController(IChatPrivateService chatPrivateService)
        {
            _chatPrivateService = chatPrivateService ?? throw new ArgumentNullException(nameof(chatPrivateService));
        }


        [HttpGet("{userIdSend}/{userIdReceive}")]

        public async Task<ActionResult<IEnumerable<ChatPrivate>>> GetAllChatsAsync(int userIdSend, int userIdReceive)
        {
            try
            {
                var chats = await _chatPrivateService.GetAllChatsAsync(userIdSend, userIdReceive);
                return Ok(chats);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddChatAsync([FromBody] ChatPrivate chat)
        {
            try
            {
                await _chatPrivateService.AddChatAsync(chat);
                return Ok("Message added successfully.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


    }
}
