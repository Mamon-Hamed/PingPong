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
        var response = await identityService.ValidateCredentialsAsync(new ValidateCredentialsRequest(request.Email, request.Password));

        if (!response.Succeeded)
            throw new BadRequestException("Invalid email or password.");

        var token = await tokenService.GenerateTokenAsync(response.UserId, response.Email, response.FullName, response.Roles, cancellationToken);

        await identityService.UpdateRefreshTokenAsync(new UpdateRefreshTokenRequest(response.UserId, token.RefreshToken, token.ExpiresAtUtc.AddDays(7)));

        var loginResponse = new LoginResponse(token.AccessToken, token.RefreshToken, token.ExpiresAtUtc);

        return Result.Success(loginResponse);
    }
}
