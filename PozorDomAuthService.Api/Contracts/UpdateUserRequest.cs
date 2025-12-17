using System.ComponentModel.DataAnnotations;

namespace PozorDomAuthService.Api.Contracts
{
    public record UpdateUserRequest(
        [MaxLength(64, ErrorMessage = "FullName cannot exceed 64 characters.")]
        string FullName
    );
}
