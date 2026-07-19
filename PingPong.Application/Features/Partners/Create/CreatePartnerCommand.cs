using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Partners;

namespace PingPong.Application.Features.Partners.Create;

public sealed record CreatePartnerCommand(
    string Name,
    string Phone,
    string MediaUrl,
    DateTime? ValidUntil,
    List<string> Gallery,
    Guid CategoryId,
    Guid CountryId,
    Guid CityId,
    CreateLocationRequest Location,
    SubscriptionStatus SubscriptionStatus) : ICommand<Guid>;

public record CreateLocationRequest(
    double Latitude,
    double Longitude,
    string City,
    string Country,
    string Address);
