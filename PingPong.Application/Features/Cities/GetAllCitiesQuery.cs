using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Cities;

public sealed record GetAllCitiesQuery : GetAllQuery<CityResponse>
{
    public string? Name { get; set; }
    public Guid? CountryId { get; set; }
}
