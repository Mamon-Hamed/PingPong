using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Countries;

public sealed record GetAllCountriesQuery : GetAllQuery<CountryResponse>
{
    public string? Name { get; set; }
}
