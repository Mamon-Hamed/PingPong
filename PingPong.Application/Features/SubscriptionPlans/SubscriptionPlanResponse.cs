namespace PingPong.Application.Features.SubscriptionPlans;

public record SubscriptionPlanResponse(
    Guid Id,
    string PlanName,
    decimal BasePrice,
    decimal DiscountPercentage,
    int DurationDays,
    decimal TotalCost);
