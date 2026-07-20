using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;
using PingPong.Infrastructure.Persistence.Configurations.Common;

namespace PingPong.Infrastructure.Persistence.Configurations;

internal sealed class CurrencyConfiguration : BaseEntityConfiguration<CurrencyEntity, CurrencyId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CurrencyEntity> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(c => c.Code)
            .HasMaxLength(StringLengths.Length64)
            .IsRequired();

        builder.Property(c => c.Symbol)
            .HasMaxLength(StringLengths.Length64)
            .IsRequired();

        builder.Property(c => c.Rate)
            .HasPrecision(18, 4);

        builder.HasIndex(c => c.Code).IsUnique();

        builder.HasIndex(c => c.IsDefault)
            .HasFilter("[IsDefault] = 1")
            .IsUnique();
    }
}