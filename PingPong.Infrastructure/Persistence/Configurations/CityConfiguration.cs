using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;
using PingPong.Infrastructure.Persistence.Converters;

namespace PingPong.Infrastructure.Persistence.Configurations;

internal sealed class CityConfiguration : BaseEntityConfiguration<CityEntity, CityId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CityEntity> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.CountryId)
            .HasConversion(new StronglyTypedIdValueConverter<CountryId>())
            .IsRequired();

        builder.HasOne(c => c.Country)
            .WithMany(country => country.Cities)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
