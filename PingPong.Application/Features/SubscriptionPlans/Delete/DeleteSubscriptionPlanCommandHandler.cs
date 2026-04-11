using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.SubscriptionPlans.Delete;

using Abstractions.Messaging;
using Domain.Repositories;
using Common;

public sealed class DeleteSubscriptionPlanCommandHandler(
    ISubscriptionPlanRepository subscriptionPlanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteSubscriptionPlanCommand>
{
    public async Task<Result> Handle(DeleteSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlanId = new SubscriptionPlanId(request.Id);
        var subscriptionPlan = await subscriptionPlanRepository.GetByIdAsync(subscriptionPlanId, cancellationToken);

        if (subscriptionPlan is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        subscriptionPlanRepository.Remove(subscriptionPlan);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
