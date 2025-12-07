using System.ComponentModel.DataAnnotations;

namespace PozorDomAuthService.Api.Contracts
{
    public record LoginRequest(
        [Required]
        [property: RegularExpression(
            @"^(\+7|8|7)[9][0-9]{9}$",
            ErrorMessage = "Invalid phone number format.")]
        string PhoneNumber);
}
