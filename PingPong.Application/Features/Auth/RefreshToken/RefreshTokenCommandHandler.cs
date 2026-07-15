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
        var fullName = principal.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName))
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

        var token = await tokenService.GenerateTokenAsync(userId, email, fullName, roles, cancellationToken);

        await identityService.UpdateRefreshTokenAsync(new UpdateRefreshTokenRequest(userId, token.RefreshToken, token.ExpiresAtUtc.AddDays(7)));

        var response = new RefreshTokenResponse(token.AccessToken, token.RefreshToken, token.ExpiresAtUtc);

        return Result.Success(response);
    }
}
