using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler(IIdentityService identityService) : ICommandHandler<RegisterCommand>
{
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await identityService.RegisterAsync(new RegisterRequest(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password));

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
