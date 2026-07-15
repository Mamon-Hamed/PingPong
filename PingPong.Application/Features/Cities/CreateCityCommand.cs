using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Cities;

public sealed record CreateCityCommand(string Name, Guid CountryId) : ICommand<Guid>;
