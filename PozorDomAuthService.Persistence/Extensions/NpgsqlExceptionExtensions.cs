using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PozorDomAuthService.Persistence.Extensions
{
    public static class NpgsqlExceptionExtensions
    {
        public static bool IsUniqueCreateConstraintViolation(this DbUpdateException exception, string constraintName)
        {
            return exception.InnerException is PostgresException pg &&
                   pg.SqlState == PostgresErrorCodes.UniqueViolation &&
                   pg.ConstraintName == constraintName;
        }

        public static bool IsUniqueUpdateKeyViolation(this PostgresException ex, string constraintName)
        {
            return ex.SqlState == PostgresErrorCodes.UniqueViolation
                && ex.ConstraintName == constraintName;
        }
    }
}
