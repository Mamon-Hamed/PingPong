using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Configurations;

internal sealed class CountryConfiguration : BaseEntityConfiguration<CountryEntity, CountryId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasMany(c => c.Cities)
            .WithOne(city => city.Country)
            .HasForeignKey(city => city.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
