using AutoMapper;
using ChaseTheFlag.Application.Users;
using ChaseTheFlag.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChaseTheFlag.Api.Controllers.Users
{

    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredUsersController : ControllerBase
    {
        private readonly IRegisteredUserService _registeredUserService;
        private readonly IMapper _mapper;

        public RegisteredUsersController(IRegisteredUserService registeredUserService, IMapper mapper)
        {
            _registeredUserService = registeredUserService ?? throw new ArgumentNullException(nameof(registeredUserService));
            _mapper = mapper;
        }




        [AllowAnonymous]
        [HttpGet("authenticate/{username}/{password}")]
        public async Task<IActionResult> AuthenticateAsync(string username, string password)
        {
            try
            {
                var token = await _registeredUserService.AuthenticateAndGetTokenAsync(username, password);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new { message = $"Invalid username or password." });

                return Ok(token);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("registry")]
        public async Task<IActionResult> Add(RegisteredUserData registeredUserData)
        {

            try
            {
                RegisteredUser registeredUser = _mapper.Map<RegisteredUser>(registeredUserData);


                registeredUser.PasswordSalt = _registeredUserService.GenerateUserSalt();
                registeredUser.PasswordHash = _registeredUserService.GenerateUserHash(registeredUserData.Password, registeredUser.PasswordSalt);
                await _registeredUserService.AddAsync(registeredUser);
                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }




        [HttpGet("{id}")]
        public async Task<ActionResult<RegisteredUser>> GetById(int id)
        {
            try
            {
                var user = await _registeredUserService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpGet("LisUserChat/{idRoom}")]
        public async Task<ActionResult<List<RegisteredUser>>> GetAllByRoomID(int idRoom)
        {
            try
            {
                return await _registeredUserService.GetAllByRoomIDAsync(idRoom);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RegisteredUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _registeredUserService.UpdateAsync(user);
            }
            catch (Exception)
            {
                if (await _registeredUserService.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await _registeredUserService.GetByIdAsync(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            await _registeredUserService.DeleteAsync(id);
            return NoContent();
        }
    }
}
