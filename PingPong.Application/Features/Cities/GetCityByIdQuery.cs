using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed record GetCityByIdQuery(Guid Id) : GetByIdQuery<CityId, CityResponse>(Id);
