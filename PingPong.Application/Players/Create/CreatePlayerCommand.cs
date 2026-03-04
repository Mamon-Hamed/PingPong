using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Players.Create;

public sealed record CreatePlayerCommand(
    string FirstName,
    string LastName,
    string Email) : ICommand<Guid>;
