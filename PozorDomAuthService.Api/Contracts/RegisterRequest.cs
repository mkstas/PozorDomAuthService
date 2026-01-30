using PozorDomAuthService.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace PozorDomAuthService.Api.Contracts
{
    public record RegisterRequest(
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_ADDRESS_LENGTH,
            ErrorMessage = "Email address cannot exceed 254 characters.")]
        string EmailAddress,

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(32, ErrorMessage = "Password cannot exceed 32 characters.")]
        string Password);
}
