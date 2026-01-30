using PozorDomAuthService.Domain.ValueObjects;

namespace PozorDomAuthService.Domain.Models
{
    public record UserId(Guid Value);

    public class User
    {
        public UserId Id { get; }

        public EmailAddress Email { get; private set; }

        public PasswordHash Password { get; private set; }

        private User(EmailAddress email, PasswordHash password)
        {
            Id = new UserId(Guid.NewGuid());
            Email = email;
            Password = password;
        }

        public static User Create(EmailAddress email, PasswordHash password)
        {
            return new User(email, password);
        }

        public void ChangeEmailAddress(EmailAddress email)
        {
            Email = email;
        }

        public void ChangePasswordHash(PasswordHash password)
        {
            Password = password;
        }
    }
}
