using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Exceptions;

namespace PingPong.Application.Auth.Register;

internal sealed class RegisterCommandHandler(IIdentityService identityService) : ICommandHandler<RegisterCommand>
{
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (succeeded, userId, errors) = await identityService.RegisterAsync(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        if (!succeeded)
        {
            var errorDict = new Dictionary<string, string[]>
            {
                { "Identity", errors }
            };
            throw new ValidationException(errorDict);
        }

        return Result.Success();
    }
}
