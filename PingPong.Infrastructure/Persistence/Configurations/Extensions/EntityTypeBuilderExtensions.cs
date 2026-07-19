using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Constants;
using PingPong.Domain.Primitives;

namespace PingPong.Infrastructure.Persistence.Configurations.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureAuditFields<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAuditableEntity
    {
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(StringLengths.UserId);

        builder.Property(e => e.CreatedByName)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(StringLengths.UserId);

        builder.Property(e => e.UpdatedByName)
            .HasMaxLength(StringLengths.Length256);

        builder.HasIndex(e => e.CreatedBy);
        builder.HasIndex(e => e.CreatedByName);
        builder.HasIndex(e => e.UpdatedBy);
        builder.HasIndex(e => e.UpdatedByName);
    }

    public static PropertyBuilder<TProperty> IsTinyEnum<TProperty>(this PropertyBuilder<TProperty> builder)
        where TProperty : struct, Enum
    {
        return builder.HasColumnType("tinyint");
    }

    public static PropertyBuilder<TProperty> HasStringEnumConversion<TProperty>(this PropertyBuilder<TProperty> builder)
        where TProperty : struct, Enum
    {
        return builder.HasConversion<string>().HasMaxLength(255);
    }
}
