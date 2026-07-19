using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Support.Update;

public sealed class UpdateSupportCommandHandler(
    ISupportRepository supportRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IIdentityService identityService)
    : ICommandHandler<UpdateSupportCommand>
{
    public async Task<Result> Handle(UpdateSupportCommand request, CancellationToken cancellationToken)
    {
        var supportId = new SupportId(request.Id);
        var supportMessage = await supportRepository.GetByIdAsync(supportId, cancellationToken);

        if (supportMessage is null)
        {
            return Result.Failure($"SupportMessage with id {request.Id} was not found.");
        }

        var userId = currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure("User is not authenticated.");
        }

        var userResult = await identityService.GetUserByIdAsync(userId);
        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Error!);
        }

        var user = userResult.Value;

        supportMessage.Update(
            user!.UserName,
            user.Email ?? string.Empty,
            request.Type,
            request.Message);

        supportRepository.Update(supportMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
