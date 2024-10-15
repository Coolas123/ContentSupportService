using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class AuthorCofiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder) {
            builder
                .ToTable("author")
                .HasKey(x=>x.UserId);

            builder
                .Property(x => x.UserId)
                .HasColumnName("user_id");

            builder
                .Ignore(x => x.Id);

            builder
                .Property(x => x.UrlPage)
                .HasColumnName("url_page")
                .HasMaxLength(64);

            builder
                .Property(x=>x.Description)
                .HasMaxLength(256);

            builder
                .HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Author>(x => x.UserId);
        }
    }
}
