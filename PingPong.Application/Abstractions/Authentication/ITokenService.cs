namespace PingPong.Application.Abstractions.Authentication;

public interface ITokenService
{
    Task<TokenResponse> GenerateTokenAsync(string userId, string email, IList<string> roles, CancellationToken cancellationToken = default);
}

public sealed record TokenResponse(string AccessToken, string RefreshToken, DateTime ExpiresAtUtc);
