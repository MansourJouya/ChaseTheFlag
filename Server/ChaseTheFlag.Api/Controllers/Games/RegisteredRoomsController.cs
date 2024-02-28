
using AutoMapper;
using ChaseTheFlag.Application.Games;
using ChaseTheFlag.Domain.Entities.Games;
using Microsoft.AspNetCore.Mvc;

namespace ChaseTheFlag.Api.Controllers.Games
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredRoomsController : ControllerBase
    {
        private readonly IRegisteredRoomService _registeredRoomService;
        private readonly IMapper _mapper;

        public RegisteredRoomsController(IRegisteredRoomService registeredRoomService, IMapper mapper)
        {
            _registeredRoomService = registeredRoomService ?? throw new ArgumentNullException(nameof(registeredRoomService));
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Room room)
        {
            try
            {
                RegisteredRoom userInRoom = _mapper.Map<RegisteredRoom>(room);

                await _registeredRoomService.AddAsync(userInRoom);
                return Ok("Room added successfully.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        [HttpGet("Rooms/{id}/{isMyRoom}")]
        public async Task<ActionResult<ICollection<RegisteredRoom>>> GetAllAsync(int id, bool isMyRoom)
        {
            try
            {
                var rooms = await _registeredRoomService.GetAllAsync(id, isMyRoom);
                return Ok(rooms);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<RegisteredRoom>> GetByIdAsync(int id)
        {
            var room = await _registeredRoomService.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, RegisteredRoom room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            try
            {
                await _registeredRoomService.UpdateAsync(room);
            }
            catch (Exception)
            {
                if (await _registeredRoomService.GetByIdAsync(id) == null)
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
            var roomToDelete = await _registeredRoomService.GetByIdAsync(id);
            if (roomToDelete == null)
            {
                return NotFound();
            }

            await _registeredRoomService.DeleteAsync(id);
            return NoContent();
        }
    }
}
