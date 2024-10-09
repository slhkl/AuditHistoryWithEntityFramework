using Business.Services.UserServices;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _userService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(User user)
        {
            await _userService.AddAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            await _userService.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            return await _userService.LoginAsync(email, password) ? Ok() : Unauthorized();
        }
    }
}
