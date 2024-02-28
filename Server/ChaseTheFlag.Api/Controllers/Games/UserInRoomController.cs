

using AutoMapper;
using ChaseTheFlag.Application.Games;
using ChaseTheFlag.Domain.Entities.Games;
using Microsoft.AspNetCore.Mvc;

namespace ChaseTheFlag.Api.Controllers.Games
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInRoomController : ControllerBase
    {
        private readonly IUserInRoomService _userInRoomService;
        private readonly IMapper _mapper;

        public UserInRoomController(IUserInRoomService userInRoomService, IMapper mapper)
        {
            _userInRoomService = userInRoomService ?? throw new ArgumentNullException(nameof(userInRoomService));
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(UserInRoom room)
        {
            try
            {
                UserInRoomData userInRoomData = new UserInRoomData()
                {
                    Id = 0,
                    UserId = room.UserId,
                    RoomId = room.RoomId
                };
                await _userInRoomService.AddAsync(userInRoomData);
                return Ok("User in room added successfully.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        public async Task<ActionResult<List<UserInRoomData>>> GetAllAsync()
        {
            var usersInRoom = await _userInRoomService.GetAllAsync();
            return usersInRoom;
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<UserInRoomData>> GetByIdAsync(int id)
        {
            var userInRoom = await _userInRoomService.GetByIdAsync(id);
            if (userInRoom == null)
            {
                return NotFound();
            }
            return userInRoom;
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UserInRoomData userInRoom)
        {
            if (id != userInRoom.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userInRoomService.UpdateAsync(userInRoom);
            }
            catch (Exception)
            {
                if (await _userInRoomService.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userInRoomToDelete = await _userInRoomService.GetByIdAsync(id);
            if (userInRoomToDelete == null)
            {
                return NotFound();
            }

            await _userInRoomService.DeleteAsync(id);
            return NoContent();
        }
    }
}

