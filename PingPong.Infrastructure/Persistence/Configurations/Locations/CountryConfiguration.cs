using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Locations;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;

namespace PingPong.Infrastructure.Persistence.Configurations.Locations;

internal sealed class CountryConfiguration : BaseEntityConfiguration<CountryEntity, CountryId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(c => c.Code)
            .HasMaxLength(StringLengths.Length10)
            .IsRequired();

        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.Code).IsUnique();

        builder.HasMany(c => c.Cities)
            .WithOne(city => city.Country)
            .HasForeignKey(city => city.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
