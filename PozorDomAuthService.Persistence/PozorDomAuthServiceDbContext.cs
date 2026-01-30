using Microsoft.EntityFrameworkCore;
using PozorDomAuthService.Domain.Models;
using PozorDomAuthService.Persistence.Configurations;

namespace PozorDomAuthService.Persistence
{
    public class PozorDomAuthServiceDbContext(
        DbContextOptions<PozorDomAuthServiceDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
