using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.SubscriptionPlans.Update;

using Abstractions.Messaging;
using Domain.Repositories;
using Common;

internal sealed class UpdateSubscriptionPlanCommandHandler(
    ISubscriptionPlanRepository subscriptionPlanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSubscriptionPlanCommand>
{
    public async Task<Result> Handle(UpdateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlanId = new SubscriptionPlanId(request.Id);
        var subscriptionPlan = await subscriptionPlanRepository.GetByIdAsync(subscriptionPlanId, cancellationToken);

        if (subscriptionPlan is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        subscriptionPlan.Update(
            request.PlanName,
            request.BasePrice,
            request.DiscountPercentage,
            request.DurationDays);

        subscriptionPlanRepository.Update(subscriptionPlan);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
