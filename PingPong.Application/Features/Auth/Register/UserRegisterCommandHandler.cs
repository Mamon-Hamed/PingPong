using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Features.Auth.Register;

public sealed class UserRegisterCommandHandler(IIdentityService identityService) : ICommandHandler<UserRegisterCommand>
{
    public async Task<Result> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await identityService.RegisterUserAsync(new UserRegisterRequest(
            request.UserName,
            request.Email,
            request.PhoneNumber,
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
