using PozorDomAuthService.Domain.Shared.Exceptions;
using System.Net;

namespace PozorDomAuthService.Api.Middlewares
{
    public class GlobalExceptionHandler(RequestDelegate next)
    {
        private class ErrorResponse(int statusCode, string message)
        {
            public int Status { get; set; } = statusCode;
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
                var response = new ErrorResponse(
                    (int)HttpStatusCode.InternalServerError, ex.Message);

                context.Response.StatusCode = ex switch
                {
                    DomainException _ => (int)HttpStatusCode.BadRequest,
                    NotFoundException _ => (int)HttpStatusCode.NotFound,
                    ConflictException _ => (int)HttpStatusCode.Conflict,
                    UnauthorizedAccessException _ => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                response.Status = context.Response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
