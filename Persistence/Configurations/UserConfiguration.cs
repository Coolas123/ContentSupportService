using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder
                .ToTable("user")
                .HasKey(x=>x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.UserName)
                .HasMaxLength(64)
                .HasColumnName("user_name");

            builder
                .Property(x => x.DateOfBirth)
                .HasColumnType("Date")
                .HasColumnName("date_of_birth");

            builder
                .Property(x => x.Email)
                .HasMaxLength(64)
                .HasColumnName("email");

            builder
                .Property(x => x.Gender)
                .HasMaxLength(32)
                .HasColumnName("gender");

            builder
                .Property(x => x.HashPassword)
                .HasColumnName("hash_password");

            builder
                .Property(x => x.SystemRole)
                .HasColumnName("system_role");

            builder
                .Property(x => x.UserRole)
                .HasColumnName("user_role");

            builder
                .Property(x => x.Country)
                .HasColumnName("country");

            builder
                .Property(x => x.WarnAmount)
                .HasColumnName("warn_amount");

            builder
                .Property(x => x.IsBan)
                .HasColumnName("is_ban");

            builder
                .HasOne(x=>x.Wallet)
                .WithOne()
                .HasForeignKey<Wallet>(x=>x.UserId);

            builder
                .HasMany(x => x.ProfileMaterials)
                .WithOne()
                .HasForeignKey(x => x.UserId);
        }
    }
}
