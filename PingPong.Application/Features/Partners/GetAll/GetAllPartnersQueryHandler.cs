using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.GetAll;

public sealed class GetAllPartnersQueryHandler(IPartnerRepository repository)
    : GetAllQueryHandler<GetAllPartnersQuery, PartnerEntity, PartnerId, PartnerResponse>(repository)
{


    protected override IQueryable<PartnerEntity> BuildQuery(GetAllPartnersQuery query)
    {
       return Queryable.FilterBase(query)
            .WhereIf(!string.IsNullOrEmpty(query.CompanyName), p => p.CompanyName.Contains(query.CompanyName!))
            .WhereIf(!string.IsNullOrEmpty(query.ContactFirstName), p => p.ContactFirstName.Contains(query.ContactFirstName!))
            .WhereIf(!string.IsNullOrEmpty(query.ContactLastName), p => p.ContactLastName.Contains(query.ContactLastName!))
            .WhereIf(!string.IsNullOrEmpty(query.Phone), p => p.Phone.Contains(query.Phone!))
            .WhereIf(!string.IsNullOrEmpty(query.Email), p => p.Email.Contains(query.Email!))
            .WhereIf(!string.IsNullOrEmpty(query.City), p => p.City.Contains(query.City!))
            .WhereIf(query.CategoryId.HasValue, p => p.CategoryId == new CategoryId(query.CategoryId!.Value))
            .WhereIf(query.Category != null, p => p.Category == query.Category)
            .WhereIf(query.IsVerified.HasValue, p => p.IsVerified == query.IsVerified)
            .WhereIf(query.SubscriptionStatus.HasValue, p => p.SubscriptionStatus == query.SubscriptionStatus);

    }
}
