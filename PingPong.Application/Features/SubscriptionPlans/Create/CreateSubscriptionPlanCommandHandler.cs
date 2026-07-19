namespace PingPong.Application.Features.SubscriptionPlans.Create;

using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Subscriptions;
using PingPong.Domain.Repositories;
using PingPong.Application.Common;

public sealed class CreateSubscriptionPlanCommandHandler(
    ISubscriptionPlanRepository subscriptionPlanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSubscriptionPlanCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = SubscriptionPlanEntity.Create(
            request.PlanName,
            request.BasePrice,
            request.DiscountPercentage,
            request.DurationDays);

        subscriptionPlanRepository.Add(subscriptionPlan);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(subscriptionPlan.Id.Value);
    }
}
