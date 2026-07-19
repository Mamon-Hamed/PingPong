using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Application.Features.Auth.Login;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Features.Auth.Register;

public sealed class AdminRegisterCommandHandler(
    IIdentityService identityService,
    ITokenService tokenService) : ICommandHandler<AdminRegisterCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await identityService.RegisterAdminAsync(new AdminRegisterRequest(
            request.UserName,
            request.Email,
            request.Password,
            request.Address,
            request.Latitude,
            request.Longitude,
            request.DeviceId,
            request.DeviceName,
            request.DeviceType,
            request.OperatingSystem,
            request.CountryId,
            request.CityId));

        if (!response.Succeeded)
        {
            var errorDict = new Dictionary<string, string[]>
            {
                { "Identity", response.Errors }
            };
            throw new ValidationException(errorDict);
        }

        var roles = new List<string> { "Admin" };

        var token = await tokenService.GenerateTokenAsync(
            response.UserId,
            request.Email,
            request.UserName,
            roles,
            request.Latitude,
            request.Longitude,
            null,
            cancellationToken);

        return Result.Success(new LoginResponse(
            token.AccessToken,
            token.RefreshToken,
            token.ExpiresAtUtc));
    }
}
