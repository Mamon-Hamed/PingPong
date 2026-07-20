using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Partners;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;
using PingPong.Infrastructure.Persistence.Converters;

namespace PingPong.Infrastructure.Persistence.Configurations.Partners;

internal sealed class PartnerServiceConfiguration : BaseEntityConfiguration<PartnerServiceEntity, ServiceId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PartnerServiceEntity> builder)
    {
        builder.Property(s => s.Name)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(s => s.Media)
            .HasMaxLength(StringLengths.Length2000);

        builder.Property(s => s.Cost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(s => s.DiscountPercentage)
            .IsRequired();

        builder.Property(s => s.PartnerId)
            .HasConversion(new StronglyTypedIdValueConverter<PartnerId>())
            .IsRequired();

        builder.Property(s => s.CurrencyId)
            .HasConversion(new StronglyTypedIdValueConverter<CurrencyId>())
            .IsRequired();

        builder.HasIndex(s => s.PartnerId);
        builder.HasIndex(s => s.CurrencyId);
        builder.HasIndex(s => s.Name);
    }
}
