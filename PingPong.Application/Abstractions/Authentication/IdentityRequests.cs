namespace PingPong.Application.Abstractions.Authentication;

public sealed record RegisterRequest(string FirstName, string LastName, string Email, string Password);

public sealed record ValidateCredentialsRequest(string Email, string Password);

public sealed record ValidateRefreshTokenRequest(string UserId, string RefreshToken);

public sealed record UpdateRefreshTokenRequest(string UserId, string NewRefreshToken, DateTime ExpiresAtUtc);
