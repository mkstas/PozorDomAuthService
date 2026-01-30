using PozorDomAuthService.Domain.Shared.Exceptions;

namespace PozorDomAuthService.Domain.ValueObjects
{
    public record PasswordHash
    {
        public string Hash { get; }

        private PasswordHash(string hash) => Hash = hash;

        public static PasswordHash Create(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new DomainException("Password hash cannot be empty.");
            }

            return new PasswordHash(hash);
        }
    }
}
