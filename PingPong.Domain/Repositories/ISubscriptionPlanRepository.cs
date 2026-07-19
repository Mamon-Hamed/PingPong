using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Entities.Subscriptions;

namespace PingPong.Domain.Repositories;

public interface ISubscriptionPlanRepository : IRepository<SubscriptionPlanEntity, SubscriptionPlanId>
{
}
