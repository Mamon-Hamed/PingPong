using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Auth.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand;
