using CSharpFunctionalExtensions;
using PozorDomAuthService.Domain.Shared;
using PozorDomAuthService.Domain.ValueObjects;

namespace PozorDomAuthService.Domain.Models
{
    public class User
    {
        private User(Guid id, EmailAddress email, PasswordHash passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }

        public Guid Id { get; }
        public EmailAddress Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        public static Result<User, Error> Create(Guid id, EmailAddress email, PasswordHash passwordHash)
        {
            if (id == Guid.Empty)
                return Result.Failure<User, Error>(new Errors.Common.ValueIsRequired(nameof(id)));

            if (email is null)
                return Result.Failure<User, Error>(new Errors.Common.ValueIsRequired(nameof(email)));

            if (passwordHash is null)
                return Result.Failure<User, Error>(new Errors.Common.ValueIsRequired(nameof(passwordHash)));

            return Result.Success<User, Error>(new User(id, email, passwordHash));
        }

        public Result<EmailAddress, Error> ChangeEmail(EmailAddress newEmail)
        {
            if (newEmail is null)
                return Result.Failure<EmailAddress, Error>(new Errors.Common.ValueIsRequired(nameof(newEmail)));

            Email = newEmail;            
            return Result.Success<EmailAddress, Error>(newEmail);
        }

        public Result<PasswordHash, Error> ChangePasswordHash(PasswordHash newPasswordHash)
        {
            if (newPasswordHash is null)
                return Result.Failure<PasswordHash, Error>(new Errors.Common.ValueIsRequired(nameof(newPasswordHash)));

            PasswordHash = newPasswordHash;
            return Result.Success<PasswordHash, Error>(newPasswordHash);
        }
    }
}
