using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder) {
            builder
                .ToTable("wallet")
                .HasKey(x=>x.UserId);

            builder
                .Property(x => x.UserId)
                .HasColumnName("user_id");

            builder
                .Ignore(x => x.Id);

            builder
                .Property(x => x.Currency)
                .HasConversion(x=>x.Name,currency => Currency.GetFromName(currency)!)
                .HasColumnName("currency_name");

            builder
                .Property(x=>x.Balance)
                .HasColumnName("balance");

            builder
                .Property(x => x.IsActive)
                .HasColumnName("is_active");
        }
    }
}
