namespace PingPong.Application.Features.SubscriptionPlans.Create;

using Abstractions.Messaging;

public sealed record CreateSubscriptionPlanCommand(
    string PlanName,
    decimal BasePrice,
    decimal DiscountPercentage,
    int DurationDays) : ICommand<Guid>;
