namespace PingPong.Application.Features.Cities;

public sealed record CityResponse(Guid Id, string Name, Guid CountryId);
