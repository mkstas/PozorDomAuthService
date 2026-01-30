using PozorDomAuthService.Domain.Shared.Exceptions;
using System.Net.Mail;

namespace PozorDomAuthService.Domain.ValueObjects
{
    public record EmailAddress
    {
        public const int MAX_ADDRESS_LENGTH = 254;

        public string Address { get; }

        private EmailAddress(string address) => Address = address;

        public static EmailAddress Create(string address)
        {
            if (address.Length > MAX_ADDRESS_LENGTH)
            {
                throw new DomainException("Email address must not exceed 254 characters.");
            }

            try
            {
                _ = new MailAddress(address);
                return new EmailAddress(address);
            }
            catch (Exception)
            {
                throw new DomainException("Invalid email address format.");
            }
        }
    }
}
