using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities.Support;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Support.Create;

public sealed class CreateSupportCommandHandler(
    ISupportRepository supportRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : ICommandHandler<CreateSupportCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSupportCommand request, CancellationToken cancellationToken)
    {
        var fromAuthenticated = !string.IsNullOrEmpty(currentUserService.UserId);

        var supportMessage = SupportMessage.Create(
            request.Name,
            request.Email,
            request.Type,
            request.Message,
            fromAuthenticated);

        supportRepository.Add(supportMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(supportMessage.Id.Value);
    }
}
