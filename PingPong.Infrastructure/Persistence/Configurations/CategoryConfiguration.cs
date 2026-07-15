using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

internal sealed class CategoryConfiguration : BaseEntityConfiguration<CategoryEntity, CategoryId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.IconUrl)
            .HasMaxLength(2000);
    }
}
