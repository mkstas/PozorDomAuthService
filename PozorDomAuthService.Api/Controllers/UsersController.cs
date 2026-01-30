using Microsoft.AspNetCore.Mvc;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Domain.Models;

namespace PozorDomAuthService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(
        IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id: Guid}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id)
        {
            var users = await _userService.GetUserByIdAsync(new UserId(id));
            return Ok(users);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeEmailAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public async Task<IActionResult> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }
    }
}
