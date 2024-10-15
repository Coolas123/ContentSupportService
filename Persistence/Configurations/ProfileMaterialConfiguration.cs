using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class ProfileMaterialConfiguration : IEntityTypeConfiguration<ProfileMaterial>
    {
        public void Configure(EntityTypeBuilder<ProfileMaterial> builder) {
            builder
                .ToTable("profile_material")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.UserId)
                .HasColumnName("user_id");

            builder
                .Property(x => x.MaterialType)
                .HasColumnName("material_type");

            builder
                .Property(x => x.Title)
                .HasMaxLength(256)
                .HasColumnName("title");

            builder
                .Property(x => x.Path)
                .HasColumnName("path");


        }
    }
}
