using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PozorDomAuthService.Api.Contracts;
using PozorDomAuthService.Api.Extensions;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Infrastructure.Providers.Jwt;

namespace PozorDomAuthService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        IUserService userService,
        IOptions<JwtOptions> jwtOptions) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.LoginOrRegisterAsync(request.PhoneNumber);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            };
            HttpContext.Response.Cookies.Append(_jwtOptions.CookieName, token, cookieOptions);

            return NoContent();
        }
        
        [HttpDelete("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete(_jwtOptions.CookieName);

            return NoContent();
        }

        [HttpGet("validate")]
        [Authorize]
        public async Task<IActionResult> Validate()
        {
            HttpContext.Response.Headers.Append("X-User-Id", User.GetUserId().ToString());

            return NoContent();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var user = await _userService.GetUserByIdAsync(User.GetUserId());
            UserResponse response = 
                new(user.Id, user.PhoneNumber, user.FullName, user.Email, user.ImageUrl);

            return Ok(response);
        }

        [HttpPatch("me/phone")]
        [Authorize]
        public async Task<IActionResult> UpdateMePhoneNumber([FromBody] UpdateUserPhoneNumberRequest request)
        {
            await _userService.UpdateUserPhoneNumberAsync(User.GetUserId(), request.PhoneNumber);

            return NoContent();
        }

        [HttpPatch("me/info")]
        [Authorize]
        public async Task<IActionResult> UpdateMeInfo([FromBody] UpdateUserInfoRequest request)
        {
            await _userService.UpdateUserInfoAsync(User.GetUserId(), request.FullName);

            return NoContent();
        }

        [HttpPatch("me/email")]
        [Authorize]
        public async Task<IActionResult> UpdateMeEmail([FromBody] UpdateUserEmailRequest request)
        {
            await _userService.UpdateUserEmailAsync(User.GetUserId(), request.Email);

            return NoContent();
        }
    }
}
