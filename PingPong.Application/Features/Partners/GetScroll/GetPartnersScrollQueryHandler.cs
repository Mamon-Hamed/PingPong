using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;
using PingPong.Application.Common;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Models;

namespace PingPong.Application.Features.Partners.GetScroll;

public sealed class GetPartnersScrollQueryHandler(IPartnerRepository repository, ICurrentUserService currentUser)
    : IQueryHandler<GetPartnersScrollQuery, PaginatedList<PartnerResponse>>
{
    public async Task<Result<PaginatedList<PartnerResponse>>> Handle(GetPartnersScrollQuery request,
        CancellationToken cancellationToken)
    {
        var query = repository.GetAsNoTrackingAsync()
            .Where(x => x.SubscriptionStatus == SubscriptionStatus.Active)
            .WhereIf(!string.IsNullOrEmpty(request.Name), p => p.Name.Contains(request.Name!))
            .WhereIf(!string.IsNullOrEmpty(request.City), p => p.Location.City.Contains(request.City!))
            .WhereIf(!string.IsNullOrEmpty(request.Country), p => p.Location.Country.Contains(request.Country!))
            .WhereIf(request.CategoryId.HasValue, p => p.CategoryId == new CategoryId(request.CategoryId!.Value))
            .WhereIf(request.IsVerified.HasValue, p => p.IsVerified == request.IsVerified)
            .WhereIf(request.SubscriptionStatus.HasValue, p => p.SubscriptionStatus == request.SubscriptionStatus);

        var userLat = request.Latitude ?? currentUser.Latitude ?? 0;
        var userLon = request.Longitude ?? currentUser.Longitude ?? 0;

        var lat1 = userLat * (Math.PI / 180.0);
        var lon1 = userLon * (Math.PI / 180.0);
        const double toRad = Math.PI / 180.0;

        query = query.OrderBy(p => (userLat == 0 && userLon == 0)
                ? 0
                : 6376500.0 * 2.0 * Math.Atan2(
                    Math.Sqrt(
                        Math.Pow(Math.Sin(((p.Location.Latitude * toRad) - lat1) / 2.0), 2.0) +
                        Math.Cos(lat1) * Math.Cos(p.Location.Latitude * toRad) *
                        Math.Pow(Math.Sin(((p.Location.Longitude * toRad) - lon1) / 2.0), 2.0)
                    ),
                    Math.Sqrt(1.0 - (
                        Math.Pow(Math.Sin(((p.Location.Latitude * toRad) - lat1) / 2.0), 2.0) +
                        Math.Cos(lat1) * Math.Cos(p.Location.Latitude * toRad) *
                        Math.Pow(Math.Sin(((p.Location.Longitude * toRad) - lon1) / 2.0), 2.0)
                    ))
                ))
            .ThenByDescending(p => p.Reviews.Average(r => r.Rating))
            .ThenByDescending(p => p.Views);

        var totalCount = await query.CountAsync(cancellationToken);

        var favoriteIds = currentUser.FavoritePartnerIds;

        var partners = await query.Select(p => new PartnerResponse(
                p.Id.Value,
                p.Name,
                p.MediaUrl,
                p.Views,
                p.ValidUntil,
                new LocationResponse(p.Location.Latitude, p.Location.Longitude, p.Location.City, p.Location.Country,
                    p.Location.Address),
                p.Reviews.Count > 0 ? p.Reviews.Average(r => r.Rating) : 0,
                p.Services.Count > 0 && p.Services.Any(s => s.DiscountPercentage > 0)
                    ? "up to " + p.Services.Max(s => s.DiscountPercentage) + "%"
                    : "",
                favoriteIds.Contains(p.Id.Value)
            ))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return Result.Success(new PaginatedList<PartnerResponse>(partners, totalCount, request.Page, request.PageSize));
    }
}