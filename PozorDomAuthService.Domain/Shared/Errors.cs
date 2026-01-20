namespace PozorDomAuthService.Domain.Shared
{
    public abstract record Error();

    public static class Errors
    {
        public static class Common
        {
            public sealed record ValueIsRequired(string FieldName) : Error;
            public sealed record InvalidValue(string FieldName, string? Details = null) : Error;
        }

        public static class User
        {
            public sealed record InvalidEmailFormat(string Email) : Error;
            public sealed record EmailAlreadyExists(string Email) : Error;
        }
    }
}
