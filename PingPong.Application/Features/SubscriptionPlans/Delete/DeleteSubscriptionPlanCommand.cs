namespace PingPong.Application.Features.SubscriptionPlans.Delete;

using Abstractions.Messaging;

public sealed record DeleteSubscriptionPlanCommand(Guid Id) : ICommand;
