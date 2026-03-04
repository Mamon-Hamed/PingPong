using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Players.GetById;

public sealed record GetPlayerByIdQuery(Guid Id) : IQuery<PlayerResponse>;

public sealed record PlayerResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAtUtc);
