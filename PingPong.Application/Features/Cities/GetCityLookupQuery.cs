using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared;

namespace PingPong.Application.Features.Cities;

public sealed record GetCityLookupQuery(Guid? CountryId = null) : IQuery<List<LookupResponse>>;
