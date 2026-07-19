using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Subscriptions;

using Primitives;

public sealed class SubscriptionPlanEntity : AggregateRoot<SubscriptionPlanId>
{
    private SubscriptionPlanEntity(
        SubscriptionPlanId id,
        string planName,
        decimal basePrice,
        decimal discountPercentage,
        int durationDays)
        : base(id)
    {
        PlanName = planName;
        BasePrice = basePrice;
        DiscountPercentage = discountPercentage;
        DurationDays = durationDays;
        TotalCost = CalculateTotalCost(basePrice, discountPercentage);
    }

    private SubscriptionPlanEntity()
    {
    }

    public string PlanName { get; private set; } = string.Empty;

    public decimal BasePrice { get; private set; }

    public decimal DiscountPercentage { get; private set; }

    public int DurationDays { get; private set; }

    public decimal TotalCost { get; private set; }

    public static SubscriptionPlanEntity Create(
        string planName,
        decimal basePrice,
        decimal discountPercentage,
        int durationDays)
    {
        var plan = new SubscriptionPlanEntity(
            SubscriptionPlanId.New(),
            planName,
            basePrice,
            discountPercentage,
            durationDays);

        return plan;
    }

    public void Update(
        string planName,
        decimal basePrice,
        decimal discountPercentage,
        int durationDays)
    {
        PlanName = planName;
        BasePrice = basePrice;
        DiscountPercentage = discountPercentage;
        DurationDays = durationDays;
        TotalCost = CalculateTotalCost(basePrice, discountPercentage);
    }

    private static decimal CalculateTotalCost(decimal basePrice, decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
        {
            throw new ArgumentException("Discount percentage must be between 0 and 100.");
        }

        var discountAmount = basePrice * (discountPercentage / 100);
        return basePrice - discountAmount;
    }
}
