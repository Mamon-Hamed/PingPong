using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.SubscriptionPlans.GetById;

using Abstractions.Messaging;
using Domain.Repositories;
using Common;

internal sealed class GetSubscriptionPlanByIdQueryHandler(ISubscriptionPlanRepository subscriptionPlanRepository)
    : IQueryHandler<GetSubscriptionPlanByIdQuery, SubscriptionPlanResponse>
{
    public async Task<Result<SubscriptionPlanResponse>> Handle(GetSubscriptionPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var subscriptionPlanId = new SubscriptionPlanId(request.Id);
        var subscriptionPlan = await subscriptionPlanRepository.GetByIdAsync(subscriptionPlanId, cancellationToken);

        if (subscriptionPlan is null)
        {
            return Result.Failure<SubscriptionPlanResponse>("The requested entity was not found.");
        }

        return Result.Success(new SubscriptionPlanResponse(
            subscriptionPlan.Id.Value,
            subscriptionPlan.PlanName,
            subscriptionPlan.BasePrice,
            subscriptionPlan.DiscountPercentage,
            subscriptionPlan.DurationDays,
            subscriptionPlan.TotalCost));
    }
}
