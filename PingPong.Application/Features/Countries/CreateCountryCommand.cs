using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Countries;

public sealed record CreateCountryCommand(string Name, string Code) : ICommand<Guid>;
