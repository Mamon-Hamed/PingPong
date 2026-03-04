using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Auth.Login;

internal sealed class LoginCommandHandler(
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

        var response = new LoginResponse(token.AccessToken, token.RefreshToken, token.ExpiresAtUtc);

        return Result.Success(response);
    }
}
