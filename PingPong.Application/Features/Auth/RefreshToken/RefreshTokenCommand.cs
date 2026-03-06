using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Auth.RefreshToken;

public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<RefreshTokenResponse>;

public sealed record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAtUtc);
