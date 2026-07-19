using System.Security.Claims;

namespace PingPong.Application.Abstractions.Authentication;

public interface ITokenService
{
    Task<TokenResponse> GenerateTokenAsync(
        string userId,
        string email,
        string userName,
        IList<string> roles,
        double? latitude = null,
        double? longitude = null,
        IList<Guid>? favoritePartnerIds = null,
        CancellationToken cancellationToken = default);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

public sealed record TokenResponse(string AccessToken, string RefreshToken, DateTime ExpiresAtUtc);
