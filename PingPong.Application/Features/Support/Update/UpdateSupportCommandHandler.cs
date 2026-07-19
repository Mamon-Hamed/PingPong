using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Support.Update;

public sealed class UpdateSupportCommandHandler(
    ISupportRepository supportRepository,
    IUnitOfWork unitOfWork)
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

        supportMessage.Update(
            request.Name,
            request.Email,
            request.Type,
            request.Message);

        supportRepository.Update(supportMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
