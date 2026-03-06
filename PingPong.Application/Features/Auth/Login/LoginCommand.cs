
using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Auth.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAtUtc);
