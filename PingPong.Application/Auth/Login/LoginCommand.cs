using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Auth.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAtUtc);
