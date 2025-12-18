using System.ComponentModel.DataAnnotations;

namespace PozorDomAuthService.Api.Contracts
{
    public record UpdateUserEmailRequest(
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        string Email
    );
}
