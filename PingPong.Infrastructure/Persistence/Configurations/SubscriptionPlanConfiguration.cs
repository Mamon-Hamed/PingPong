using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

internal sealed class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlanEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlanEntity> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Id)
            .HasConversion(
                planId => planId.Value,
                value => new SubscriptionPlanId(value));

        builder.Property(sp => sp.PlanName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(sp => sp.BasePrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(sp => sp.DiscountPercentage)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(sp => sp.DurationDays)
            .IsRequired();

        builder.Property(sp => sp.TotalCost)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}
