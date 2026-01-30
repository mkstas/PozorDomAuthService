using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PozorDomAuthService.Domain.Models;
using PozorDomAuthService.Domain.ValueObjects;

namespace PozorDomAuthService.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Id).HasColumnName("id");

            builder.ComplexProperty(u => u.Email, b =>
            {
                b.IsRequired();
                b.Property(e => e.Address)
                 .HasMaxLength(EmailAddress.MAX_ADDRESS_LENGTH)
                 .HasColumnName("email_address");
            });

            builder.ComplexProperty(u => u.Password, b =>
            {
                b.IsRequired();
                b.Property(p => p.Hash).HasColumnName("password_hash");
            });
        }
    }
}
