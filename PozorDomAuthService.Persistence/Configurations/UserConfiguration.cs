using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(16);

            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique();

            builder.Property(u => u.FullName)
                   .HasMaxLength(64)
                   .HasDefaultValue(string.Empty);

            builder.Property(u => u.Email)
                   .HasDefaultValue(string.Empty);

            builder.Property(u => u.ImageUrl)
                   .HasDefaultValue(string.Empty);
        }
    }
}
