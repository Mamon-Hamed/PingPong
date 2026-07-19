using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Partners;

namespace PingPong.Application.Features.Partners.Update;

public sealed record UpdatePartnerCommand(
    Guid Id,
    string Name,
    string Phone,
    string MediaUrl,
    DateTime? ValidUntil,
    List<string> Gallery,
    Guid CategoryId,
    Guid CountryId,
    Guid CityId,
    UpdateLocationRequest Location,
    SubscriptionStatus SubscriptionStatus) : ICommand;

public record UpdateLocationRequest(
    double Latitude,
    double Longitude,
    string City,
    string Country,
    string Address);
