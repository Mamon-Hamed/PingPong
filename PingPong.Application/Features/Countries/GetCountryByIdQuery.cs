using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Countries;

public sealed record GetCountryByIdQuery(Guid Id) : GetByIdQuery<CountryId, CountryResponse>(Id);
