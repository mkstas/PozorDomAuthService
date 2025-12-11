using Microsoft.AspNetCore.Mvc;
using PozorDomAuthService.Api.Contracts;
using PozorDomAuthService.Domain.Interfaces.Services;

namespace PozorDomAuthService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(
        IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _userService.LoginOrRegisterAsync(request.PhoneNumber);

                HttpContext.Response.Cookies.Append("very-non-secret-cookie", token);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Results.InternalServerError();
            }
        }

        [HttpDelete("logout")]
        public async Task<IResult> Delete()
        {
            HttpContext.Response.Cookies.Delete("very-non-secret-cookie");

            return Results.NoContent();
        }
    }
}
