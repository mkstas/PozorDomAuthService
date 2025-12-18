using System.ComponentModel.DataAnnotations;

namespace PozorDomAuthService.Api.Contracts
{
    public record UpdateUserPhoneNumberRequest(
        [Required]
        [RegularExpression(@"^\+7\d{10}$",
            ErrorMessage = "Phone number must be in the format +7XXXXXXXXXX.")]
        string PhoneNumber
    );
}
