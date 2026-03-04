using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Auth.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand;
