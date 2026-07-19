using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.StronglyTypes;
using PingPong.Application.Common;

namespace PingPong.Application.Features.Partners.GetById;

public sealed class GetPartnerByIdQueryHandler(IPartnerRepository partnerRepository)
    : IQueryHandler<GetPartnerByIdQuery, PartnerResponse>
{
    public async Task<Result<PartnerResponse>> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
    {
        var partnerId = new PartnerId(request.Id);
        var partner = await partnerRepository.GetByIdAsync(partnerId, cancellationToken);

        if (partner is null)
        {
            return Result.Failure<PartnerResponse>("The requested entity was not found.");
        }

        var response = MapToResponse(partner, request.UserLatitude, request.UserLongitude);

        return Result.Success(response);
    }

    private PartnerResponse MapToResponse(PartnerEntity entity, double userLat, double userLon)
    {
        var rating = entity.Reviews.Any() ? entity.Reviews.Average(r => r.Rating) : 0;
        var distance = CalculateDistance(userLat, userLon, entity.Location.Latitude, entity.Location.Longitude);

        decimal? minDiscount = entity.Services.Any() ? entity.Services.Min(s => (decimal?)s.DiscountPercentage) : null;
        decimal? maxDiscount = entity.Services.Any() ? entity.Services.Max(s => (decimal?)s.DiscountPercentage) : null;

        string discountText = "";
        if (maxDiscount.HasValue)
        {
            discountText = $"up to {maxDiscount.Value}%";
        }

        return new PartnerResponse(
            entity.Id.Value,
            entity.Name,
            entity.Phone,
            entity.MediaUrl,
            entity.ValidUntil,
            entity.Gallery,
            entity.CategoryId.Value,
            entity.CountryId.Value,
            entity.CityId.Value,
            new LocationResponse(
                entity.Location.Latitude,
                entity.Location.Longitude,
                entity.Location.City,
                entity.Location.Country,
                entity.Location.Address),
            entity.Views,
            entity.IsVerified,
            entity.SubscriptionStatus,
            entity.OpeningHours.Select(oh => new OpeningHourResponse(oh.Day, oh.Start, oh.End, oh.IsClosed)).ToList(),
            entity.Services.Select(s => new ServiceResponse(s.Id.Value, s.Name, s.Media, s.Cost, s.DiscountPercentage, s.CostAfterDiscount)).ToList(),
            entity.Reviews.Select(r => new ReviewResponse(r.Id.Value, r.AuthorName, r.AuthorAvatar, r.Rating, r.Comment, r.Date)).ToList(),
            rating,
            distance,
            discountText);
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var d1 = lat1 * (Math.PI / 180.0);
        var num1 = lon1 * (Math.PI / 180.0);
        var d2 = lat2 * (Math.PI / 180.0);
        var num2 = lon2 * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
        return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }
}
