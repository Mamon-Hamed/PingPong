using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;

namespace PingPong.Infrastructure.Persistence.Configurations.Subscriptions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Subscriptions;
using Common;

internal sealed class SubscriptionPlanConfiguration : BaseEntityConfiguration<SubscriptionPlanEntity, SubscriptionPlanId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SubscriptionPlanEntity> builder)
    {
        builder.Property(sp => sp.PlanName)
            .HasMaxLength(StringLengths.Length200)
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

        builder.HasIndex(sp => sp.PlanName);
    }
}
