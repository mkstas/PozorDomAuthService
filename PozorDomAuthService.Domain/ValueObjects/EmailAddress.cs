using CSharpFunctionalExtensions;
using PozorDomAuthService.Domain.Shared;

namespace PozorDomAuthService.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        public string Email { get; }

        private EmailAddress(string email)
        {
            Email = email;
        }

        public static Result<EmailAddress, Error> Create(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);

                if (addr.Address != email)
                    return Result.Failure<EmailAddress, Error>(new Errors.User.InvalidEmailFormat(email));
            }
            catch (Exception)
            {
                return Result.Failure<EmailAddress, Error>(new Errors.User.InvalidEmailFormat(email));
            }

            return Result.Success<EmailAddress, Error>(new EmailAddress(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}
