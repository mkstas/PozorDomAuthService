using PozorDomAuthService.Infrastructure.Exceptions;
using System.Net;

namespace PozorDomAuthService.Api.Middlewares
{
    public class GlobalExceptionHandler(RequestDelegate next)
    {
        private class ErrorResponse(int statusCode, string message)
        {
            public int StatusCode { get; set; } = statusCode;
            public string Message { get; set; } = message;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught in GlobalExceptionHandler: {ex}");
                var response = new ErrorResponse(
                    (int)HttpStatusCode.InternalServerError, ex.Message);

                context.Response.StatusCode = ex switch
                {
                    NotFoundException _ => (int)HttpStatusCode.NotFound,
                    ConflictException _ => (int)HttpStatusCode.Conflict,
                    UnauthorizedAccessException _ => (int)HttpStatusCode.Unauthorized,
                    InternalServerException _ => (int)HttpStatusCode.InternalServerError,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                response.StatusCode = context.Response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
