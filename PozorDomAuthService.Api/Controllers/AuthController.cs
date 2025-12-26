using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PozorDomAuthService.Api.Contracts;
using PozorDomAuthService.Api.Extensions;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Infrastructure.Common;

namespace PozorDomAuthService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(
        IUserService userService,
        IOptions<JwtOptions> jwtOptions) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginRequest request)
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

            return Results.NoContent();
        }
        
        [HttpDelete("logout")]
        [Authorize]
        public async Task<IResult> Logout()
        {
            HttpContext.Response.Cookies.Delete(_jwtOptions.CookieName);

            return Results.NoContent();
        }

        [HttpGet("validate")]
        [Authorize]
        public async Task<IResult> Validate()
        {
            HttpContext.Response.Headers.Append("X-User-Id", User.GetUserId().ToString());

            return Results.NoContent();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IResult> GetMe()
        {
            var user = await _userService.GetUserByIdAsync(User.GetUserId());
            UserResponse response = 
                new(user.Id, user.PhoneNumber, user.FullName, user.Email, user.ImageUrl);

            return Results.Ok(response);
        }

        [HttpPatch("me/phone")]
        [Authorize]
        public async Task<IResult> UpdateMePhoneNumber([FromBody] UpdateUserPhoneNumberRequest request)
        {
            await _userService.UpdateUserPhoneNumberAsync(User.GetUserId(), request.PhoneNumber);

            return Results.NoContent();
        }

        [HttpPatch("me/info")]
        [Authorize]
        public async Task<IResult> UpdateMeInfo([FromBody] UpdateUserInfoRequest request)
        {
            await _userService.UpdateUserInfoAsync(User.GetUserId(), request.FullName);

            return Results.NoContent();
        }

        [HttpPatch("me/email")]
        [Authorize]
        public async Task<IResult> UpdateMeEmail([FromBody] UpdateUserEmailRequest request)
        {
            await _userService.UpdateUserEmailAsync(User.GetUserId(), request.Email);

            return Results.NoContent();
        }

        [HttpPatch("me/image")]
        [Authorize]
        public async Task<IResult> UpdateMeImage(IFormFile image)
        {
            await _userService.UpdateUserImageUrlAsync(User.GetUserId(), image);

            return Results.NoContent();
        }
    }
}
