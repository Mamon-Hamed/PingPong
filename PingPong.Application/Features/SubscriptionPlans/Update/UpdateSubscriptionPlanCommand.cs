namespace PingPong.Application.Features.SubscriptionPlans.Update;

using Abstractions.Messaging;

public sealed record UpdateSubscriptionPlanCommand(
    Guid Id,
    string PlanName,
    decimal BasePrice,
    decimal DiscountPercentage,
    int DurationDays) : ICommand;
