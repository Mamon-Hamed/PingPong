using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

internal sealed class SubscriptionPlanConfiguration : BaseEntityConfiguration<SubscriptionPlanEntity, SubscriptionPlanId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SubscriptionPlanEntity> builder)
    {
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
