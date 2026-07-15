using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Features.Auth.Register;

public sealed class AdminRegisterCommandHandler(IIdentityService identityService) : ICommandHandler<AdminRegisterCommand>
{
    public async Task<Result> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
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

        return Result.Success();
    }
}
