using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Locations;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;
using PingPong.Infrastructure.Persistence.Converters;

namespace PingPong.Infrastructure.Persistence.Configurations.Locations;

internal sealed class CityConfiguration : BaseEntityConfiguration<CityEntity, CityId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CityEntity> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(c => c.CountryId)
            .HasConversion(new StronglyTypedIdValueConverter<CountryId>())
            .IsRequired();

        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.CountryId);

        builder.HasOne(c => c.Country)
            .WithMany(country => country.Cities)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
