using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Repositories;

using Domain.Entities.Subscriptions;
using PingPong.Domain.Repositories;

internal sealed class SubscriptionPlanRepository(AppDbContext dbContext)
    : Repository<SubscriptionPlanEntity, SubscriptionPlanId>(dbContext), ISubscriptionPlanRepository
{
}
