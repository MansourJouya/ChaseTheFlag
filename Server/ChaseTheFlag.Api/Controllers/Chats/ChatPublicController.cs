
using ChaseTheFlag.Application.Chats;
using ChaseTheFlag.Domain.Entities.Chats;

using Microsoft.AspNetCore.Mvc;

namespace ChaseTheFlag.Api.Controllers.Chats
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatPublicController : ControllerBase
    {
        private readonly IChatPublicService _chatPublicService;

        public ChatPublicController(IChatPublicService chatPublicService)
        {
            _chatPublicService = chatPublicService ?? throw new ArgumentNullException(nameof(chatPublicService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatPublic>>> GetAllChatsAsync()
        {
            try
            {
                var chats = await _chatPublicService.GetAllChatsAsync();
                return Ok(chats);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddChatAsync([FromBody] ChatPublic chat)
        {
            try
            {
                await _chatPublicService.AddChatAsync(chat);
                return Ok("Message added successfully.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
