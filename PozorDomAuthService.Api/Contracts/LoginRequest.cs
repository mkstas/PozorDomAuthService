using System.ComponentModel.DataAnnotations;

namespace PozorDomAuthService.Api.Contracts
{
    public record LoginRequest(
        [Required]
        string PhoneNumber);
}
