using Microsoft.EntityFrameworkCore;
using Mapster;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;
using PingPong.Application.Common;

using PingPong.Application.Abstractions.Authentication;

namespace PingPong.Application.Features.Partners.GetById;

public sealed class GetPartnerByIdQueryHandler(IPartnerRepository partnerRepository, ICurrentUserService currentUser)
    : IQueryHandler<GetPartnerByIdQuery, PartnerDetailsResponse>
{
    public async Task<Result<PartnerDetailsResponse>> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
    {
        var partnerId = new PartnerId(request.Id);
        var partner = await partnerRepository.GetAsNoTrackingAsync()
            .Where(p => p.Id == partnerId)
            .ProjectToType<PartnerDetailsResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        if (partner is null)
        {
            return Result.Failure<PartnerDetailsResponse>("The requested entity was not found.");
        }

        var isFavorite = currentUser.FavoritePartnerIds.Contains(partner.Id);
        partner = partner with { IsFavorite = isFavorite };

        var userLat = request.UserLatitude;
        var userLon = request.UserLongitude;
        
        if (userLat == 0 && userLon == 0)
        {
            return Result.Success(partner);
        }

        const double toRad = Math.PI / 180.0;
        var lat1 = userLat * toRad;
        var lon1 = userLon * toRad;
        var lat2 = partner.Location.Latitude * toRad;
        var lon2 = partner.Location.Longitude * toRad;

        var distance = 6376500.0 * 2.0 * Math.Atan2(
            Math.Sqrt(
                Math.Pow(Math.Sin((lat2 - lat1) / 2.0), 2.0) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lon2 - lon1) / 2.0), 2.0)
            ),
            Math.Sqrt(1.0 - (
                Math.Pow(Math.Sin((lat2 - lat1) / 2.0), 2.0) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lon2 - lon1) / 2.0), 2.0)
            ))
        );

        return Result.Success(partner with { Distance = distance });
    }
}
