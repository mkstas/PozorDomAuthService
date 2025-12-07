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
                   .HasMaxLength(11);

            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique();

            builder.Property(u => u.CreatedAt)
                   .HasColumnType("timestamp with time zone")
                   .HasDefaultValueSql("NOW()")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(u => u.UpdatedAt)
                   .HasColumnType("timestamp with time zone")
                   .HasDefaultValueSql("NOW()")
                   .ValueGeneratedOnAddOrUpdate()
                   .IsRequired();
        }
    }
}
