using CSharpFunctionalExtensions;
using PozorDomAuthService.Domain.Shared;

namespace PozorDomAuthService.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        public string Hash { get; }

        private PasswordHash(string hash)
        {
            Hash = hash;
        }

        public static Result<PasswordHash, Error> Create(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return Result.Failure<PasswordHash, Error>(new Errors.Common.ValueIsRequired(nameof(PasswordHash)));

            return Result.Success<PasswordHash, Error>(new PasswordHash(hash));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
        }
    }
}
