using PingPong.Domain.Entities.Partners;

namespace PingPong.Application.Features.Partners;

public record PartnerResponse(
    Guid Id,
    string Name,
    string mediaUrl,
    int Views,
    DateTime? ValidUntil,
    LocationResponse Location,
    double Rating,
    int ReviewsCount,
    string DiscountText,
    bool isFavorite
);
public record PartnerDetailsResponse(
    Guid Id,
    string Name,
    string Phone,
    string MediaUrl,
    DateTime? ValidUntil,
    List<string> Gallery,
    Guid CategoryId,
    Guid CountryId,
    Guid CityId,
    LocationResponse Location,
    int Views,
    bool IsVerified,
    SubscriptionStatus SubscriptionStatus,
    List<OpeningHourResponse> OpeningHours,
    List<ServiceResponse> Services,
    List<ReviewResponse> Reviews,
    int ReviewsCount,
    double Rating,
    double Distance,
    string DiscountText,
    bool IsFavorite);

public record LocationResponse(
    double Latitude,
    double Longitude,
    string City,
    string Country,
    string Address);

public record OpeningHourResponse(
    DayOfWeek Day,
    string Start,
    string End,
    bool IsClosed);

public record ServiceResponse(
    Guid Id,
    string Name,
    string Media,
    decimal Cost,
    double DiscountPercentage,
    decimal CostAfterDiscount,
    string CurrencyCode,
    string CurrencySymbol,
    decimal CurrencyRate);

public record ReviewResponse(
    Guid Id,
    string AuthorName,
    string AuthorAvatar,
    double Rating,
    string Comment,
    DateTime Date);
