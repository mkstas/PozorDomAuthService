namespace PozorDomAuthService.Api.Contracts
{
    public record UserResponse(
        Guid Id,
        string PhoneNumber,
        string FullName,
        string Email,
        string ImageUrl
    );
}
