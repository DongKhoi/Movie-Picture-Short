using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Movie_Web_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO?>> GetUserAsync([FromQuery] Guid id)
        {
            return await _userService.GetUser(id);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Response<Guid>>> RegisterUserAsync([FromBody] UserDTO userDTO)
        {
            return await _userService.Register(userDTO);
        }
    }
}
