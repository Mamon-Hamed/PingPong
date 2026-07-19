using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;

namespace PingPong.Infrastructure.Persistence.Configurations.Categories;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Categories;
using Common;

internal sealed class CategoryConfiguration : BaseEntityConfiguration<CategoryEntity, CategoryId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(c => c.IconUrl)
            .HasMaxLength(StringLengths.Length2000);

        builder.Property(c => c.Color)
            .HasMaxLength(StringLengths.Length64);

        builder.HasIndex(c => c.Name);
    }
}
