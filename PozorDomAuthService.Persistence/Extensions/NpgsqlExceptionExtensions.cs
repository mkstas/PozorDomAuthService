using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PozorDomAuthService.Persistence.Extensions
{
    public static class NpgsqlExceptionExtensions
    {
        public static bool IsUniqueKeyViolation(this DbUpdateException ex, string uniqueIndexName)
        {
            return ex.InnerException is PostgresException pg
                && pg.SqlState == PostgresErrorCodes.UniqueViolation
                && pg.ConstraintName == uniqueIndexName;
        }
    }
}
