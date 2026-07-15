using System.Security.Claims;

namespace PingPong.Application.Abstractions.Authentication;

public interface ITokenService
{
    Task<TokenResponse> GenerateTokenAsync(string userId, string email, string fullName, IList<string> roles, CancellationToken cancellationToken = default);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

public sealed record TokenResponse(string AccessToken, string RefreshToken, DateTime ExpiresAtUtc);
