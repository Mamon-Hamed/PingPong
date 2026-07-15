using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Cities;

public sealed record UpdateCityCommand(Guid Id, string Name, Guid CountryId) : ICommand;
