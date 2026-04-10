using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Configurations;

using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

internal sealed class PartnerConfiguration : IEntityTypeConfiguration<PartnerEntity>
{
    public void Configure(EntityTypeBuilder<PartnerEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                partnerId => partnerId.Value,
                value => new PartnerId(value));

        builder.Property(p => p.CompanyName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.ContactFirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ContactLastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .HasConversion(
                categoryId => categoryId.Value,
                value => new CategoryId(value))
            .IsRequired();

        builder.Property(p => p.Photos)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
            .Metadata.SetValueComparer(
                new ValueComparer<List<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

        builder.Property(p => p.IsVerified)
            .IsRequired();

        builder.Property(p => p.SubscriptionStatus)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
