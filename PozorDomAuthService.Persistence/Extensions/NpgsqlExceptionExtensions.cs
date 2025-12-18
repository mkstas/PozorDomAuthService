using Npgsql;

namespace PozorDomAuthService.Persistence.Extensions
{
    public static class NpgsqlExceptionExtensions
    {
        public static bool IsUniqueKeyViolation(this PostgresException ex, string uniqueIndexName)
        {
            return ex.SqlState == PostgresErrorCodes.UniqueViolation
                && ex.ConstraintName == uniqueIndexName;
        }
    }
}
