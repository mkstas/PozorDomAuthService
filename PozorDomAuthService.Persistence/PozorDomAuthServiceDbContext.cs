using Microsoft.EntityFrameworkCore;
using PozorDomAuthService.Domain.Entities;
using PozorDomAuthService.Persistence.Configurations;

namespace PozorDomAuthService.Persistence
{
    public class PozorDomAuthServiceDbContext(
        DbContextOptions<PozorDomAuthServiceDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
