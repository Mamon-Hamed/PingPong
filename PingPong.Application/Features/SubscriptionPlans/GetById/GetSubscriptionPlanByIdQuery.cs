namespace PingPong.Application.Features.SubscriptionPlans.GetById;

using Abstractions.Messaging;

public sealed record GetSubscriptionPlanByIdQuery(Guid Id) : IQuery<SubscriptionPlanResponse>;
