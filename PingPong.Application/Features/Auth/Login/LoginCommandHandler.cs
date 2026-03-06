using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Features.Auth.Login;

public sealed class LoginCommandHandler(
    IIdentityService identityService,
    ITokenService tokenService) : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (succeeded, userId, email, roles) =
            await identityService.ValidateCredentialsAsync(request.Email, request.Password);

        if (!succeeded)
            throw new BadRequestException("Invalid email or password.");

        var token = await tokenService.GenerateTokenAsync(userId, email, roles, cancellationToken);

        await identityService.UpdateRefreshTokenAsync(userId, token.RefreshToken, token.ExpiresAtUtc.AddDays(7)); // Or use config for expiry

        var response = new LoginResponse(token.AccessToken, token.RefreshToken, token.ExpiresAtUtc);

        return Result.Success(response);
    }
}
