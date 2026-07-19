using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.GetAll;

public sealed class GetAllPartnersQueryHandler(IPartnerRepository repository)
    : GetAllQueryHandler<GetAllPartnersQuery, PartnerEntity, PartnerId, PartnerResponse>(repository)
{


    protected override IQueryable<PartnerEntity> BuildQuery(GetAllPartnersQuery query)
    {
       return Queryable.FilterBase(query)
            .WhereIf(!string.IsNullOrEmpty(query.Name), p => p.Name.Contains(query.Name!))
            .WhereIf(!string.IsNullOrEmpty(query.Phone), p => p.Phone.Contains(query.Phone!))
            .WhereIf(!string.IsNullOrEmpty(query.City), p => p.Location.City.Contains(query.City!))
            .WhereIf(!string.IsNullOrEmpty(query.Country), p => p.Location.Country.Contains(query.Country!))
            .WhereIf(query.CategoryId.HasValue, p => p.CategoryId == new CategoryId(query.CategoryId!.Value))
            .WhereIf(query.Category != null, p => p.Category == query.Category)
            .WhereIf(query.IsVerified.HasValue, p => p.IsVerified == query.IsVerified)
            .WhereIf(query.SubscriptionStatus.HasValue, p => p.SubscriptionStatus == query.SubscriptionStatus);
    }
}
