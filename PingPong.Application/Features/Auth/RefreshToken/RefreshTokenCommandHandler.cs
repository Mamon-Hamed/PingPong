using System.Security.Claims;
using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Features.Auth.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    IIdentityService identityService,
    ITokenService tokenService) : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        var userName = principal.FindFirst(ClaimTypes.Name)?.Value;
        var latitudeStr = principal.FindFirst("latitude")?.Value;
        var longitudeStr = principal.FindFirst("longitude")?.Value;

        double? latitude = double.TryParse(latitudeStr, out var lat) ? lat : null;
        double? longitude = double.TryParse(longitudeStr, out var lon) ? lon : null;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName))
        {
            throw new BadRequestException("Invalid access token.");
        }

        var isValidRefreshToken = await identityService.ValidateRefreshTokenAsync(new ValidateRefreshTokenRequest(userId, request.RefreshToken));

        if (!isValidRefreshToken)
        {
            throw new BadRequestException("Invalid refresh token.");
        }

        var roles = principal.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

        var favoritePartnerIds = principal.Claims
            .Where(c => c.Type == "favorite_partner_id")
            .Select(c => Guid.TryParse(c.Value, out var guid) ? guid : Guid.Empty)
            .Where(g => g != Guid.Empty)
            .ToList();

        var token = await tokenService.GenerateTokenAsync(userId, email, userName, roles, latitude, longitude, favoritePartnerIds, cancellationToken);

        await identityService.UpdateLastLoginAsync(userId);

        await identityService.UpdateRefreshTokenAsync(new UpdateRefreshTokenRequest(userId, token.RefreshToken, token.ExpiresAtUtc.AddDays(7)));

        var response = new RefreshTokenResponse(token.AccessToken, token.RefreshToken, token.ExpiresAtUtc);

        return Result.Success(response);
    }
}
