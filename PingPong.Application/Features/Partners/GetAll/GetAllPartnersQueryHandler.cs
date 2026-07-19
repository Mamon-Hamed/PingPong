using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Models;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.GetAll;

public sealed class GetAllPartnersQueryHandler(IPartnerRepository repository, ICurrentUserService currentUser)
    : GetAllQueryHandler<GetAllPartnersQuery, PartnerEntity, PartnerId, PartnerDetailsResponse>(repository)
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

    public override async Task<Result<PaginatedList<PartnerDetailsResponse>>> Handle(GetAllPartnersQuery request, CancellationToken cancellationToken)
    {
        var result = await base.Handle(request, cancellationToken);
        if (!result.IsSuccess) return result;

        var favoriteIds = currentUser.FavoritePartnerIds;
        var updatedItems = result.Value!.Items.Select(p => p with { IsFavorite = favoriteIds.Contains(p.Id) }).ToList();
        
        // PaginatedList constructor: (items, count, pageNumber, pageSize)
        // We don't have direct access to pageSize in the result, but we have it in the request.
        return Result.Success(new PaginatedList<PartnerDetailsResponse>(updatedItems, result.Value.TotalItems, result.Value.PageNumber, request.PageSize));
    }
}
