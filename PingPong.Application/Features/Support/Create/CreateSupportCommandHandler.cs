using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities.Support;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Support.Create;

public sealed class CreateSupportCommandHandler(
    ISupportRepository supportRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IIdentityService identityService)
    : ICommandHandler<CreateSupportCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSupportCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure<Guid>("User is not authenticated.");
        }

        var userResult = await identityService.GetUserByIdAsync(userId);
        if (userResult.IsFailure)
        {
            return Result.Failure<Guid>(userResult.Error!);
        }

        var user = userResult.Value;

        var supportMessage = SupportMessageEntity.Create(
            user!.UserName,
            user.Email,
            request.Type,
            request.Message,
            true);

        supportRepository.Add(supportMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(supportMessage.Id.Value);
    }
}
