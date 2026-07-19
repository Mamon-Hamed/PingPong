using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Partners;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Infrastructure.Persistence.Configurations.Extensions;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;
using PingPong.Infrastructure.Persistence.Converters;

namespace PingPong.Infrastructure.Persistence.Configurations.Partners;

internal sealed class PartnerOpeningHourConfiguration : BaseEntityConfiguration<PartnerOpeningHour, OpeningHourId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PartnerOpeningHour> builder)
    {
        builder.Property(oh => oh.Day)
            .IsTinyEnum()
            .IsRequired();

        builder.Property(oh => oh.Start)
            .HasMaxLength(StringLengths.Length5);

        builder.Property(oh => oh.End)
            .HasMaxLength(StringLengths.Length5);

        builder.Property(oh => oh.IsClosed)
            .IsRequired();

        builder.Property(oh => oh.PartnerId)
            .HasConversion(new StronglyTypedIdValueConverter<PartnerId>())
            .IsRequired();

        builder.HasIndex(oh => oh.PartnerId);
    }
}
