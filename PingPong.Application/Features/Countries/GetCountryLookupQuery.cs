using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared;

namespace PingPong.Application.Features.Countries;

public sealed record GetCountryLookupQuery : IQuery<List<LookupResponse>>;
