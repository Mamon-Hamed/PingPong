using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Entities;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.SubscriptionPlans.GetAll;


internal sealed class GetAllSubscriptionPlansQueryHandler(ISubscriptionPlanRepository repository)
    : GetAllQueryHandler<GetAllSubscriptionPlansQuery,SubscriptionPlanEntity, SubscriptionPlanId, SubscriptionPlanResponse>(repository)
{
    protected override IQueryable<SubscriptionPlanEntity> BuildQuery(GetAllSubscriptionPlansQuery query)
    {
        return Queryable.FilterBase(query)
            .WhereIf(!string.IsNullOrEmpty(query.PlanName), sp => sp.PlanName.Contains(query.PlanName!))
            .WhereIf(query.MinBasePrice.HasValue, sp => sp.BasePrice >= query.MinBasePrice!.Value)
            .WhereIf(query.MaxBasePrice.HasValue, sp => sp.BasePrice <= query.MaxBasePrice!.Value)
            .WhereIf(query.MinDiscountPercentage.HasValue, sp => sp.DiscountPercentage >= query.MinDiscountPercentage!.Value)
            .WhereIf(query.MaxDiscountPercentage.HasValue, sp => sp.DiscountPercentage <= query.MaxDiscountPercentage!.Value)
            .WhereIf(query.MinDurationDays.HasValue, sp => sp.DurationDays >= query.MinDurationDays!.Value)
            .WhereIf(query.MaxDurationDays.HasValue, sp => sp.DurationDays <= query.MaxDurationDays!.Value)
            .WhereIf(query.MinTotalCost.HasValue, sp => sp.TotalCost >= query.MinTotalCost!.Value)
            .WhereIf(query.MaxTotalCost.HasValue, sp => sp.TotalCost <= query.MaxTotalCost!.Value);
    }
}
