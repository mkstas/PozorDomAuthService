namespace PozorDomAuthService.Domain.Entities
{
    public class UserEntity(Guid id, string phoneNumber)
    {
        public Guid Id { get; set; } = id;
        public string PhoneNumber { get; set; } = phoneNumber;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
