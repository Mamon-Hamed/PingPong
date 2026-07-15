using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Countries;

public sealed record UpdateCountryCommand(Guid Id, string Name, string Code) : ICommand;
