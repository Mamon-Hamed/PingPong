using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Partners;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Infrastructure.Persistence.Configurations.Extensions;
using PingPong.Infrastructure.Identity;
using PingPong.Infrastructure.Persistence.Converters;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;

namespace PingPong.Infrastructure.Persistence.Configurations.Partners;

internal sealed class PartnerConfiguration : BaseEntityConfiguration<PartnerEntity, PartnerId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PartnerEntity> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(p => p.Phone)
            .HasMaxLength(StringLengths.Length20)
            .IsRequired();

        builder.Property(p => p.MediaUrl)
            .HasMaxLength(StringLengths.Length2000);

        builder.Property(p => p.ValidUntil);

        builder.Property(p => p.CategoryId)
            .HasConversion(new StronglyTypedIdValueConverter<CategoryId>())
            .IsRequired();

        builder.Property(p => p.CountryId)
            .HasConversion(new StronglyTypedIdValueConverter<CountryId>())
            .IsRequired();

        builder.Property(p => p.CityId)
            .HasConversion(new StronglyTypedIdValueConverter<CityId>())
            .IsRequired();

        builder.Property(p => p.Gallery)
            .HasMaxLength(StringLengths.Length2000)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
            .Metadata.SetValueComparer(
                new ValueComparer<List<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

        builder.OwnsOne(p => p.Location, location =>
        {
            location.Property(l => l.Latitude).IsRequired();
            location.Property(l => l.Longitude).IsRequired();
            location.Property(l => l.City).HasMaxLength(StringLengths.Length100).IsRequired();
            location.Property(l => l.Country).HasMaxLength(StringLengths.Length100).IsRequired();
            location.Property(l => l.Address).HasMaxLength(StringLengths.Length500).IsRequired();
            
            location.HasIndex(l => new { l.Latitude, l.Longitude });
        });

        builder.Property(p => p.Views)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.IsVerified)
            .IsRequired();

        builder.Property(p => p.SubscriptionStatus)
            .IsTinyEnum()
            .IsRequired();

        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.CountryId);
        builder.HasIndex(p => p.CityId);
        builder.HasIndex(p => p.SubscriptionStatus);
        builder.HasIndex(p => p.IsVerified);

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.City)
            .WithMany()
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.OpeningHours)
            .WithOne(oh => oh.Partner)
            .HasForeignKey(oh => oh.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Services)
            .WithOne(s => s.Partner)
            .HasForeignKey(s => s.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Partner)
            .HasForeignKey(r => r.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<UserFavoritePartner>()
            .WithOne(fp => fp.Partner)
            .HasForeignKey(fp => fp.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
