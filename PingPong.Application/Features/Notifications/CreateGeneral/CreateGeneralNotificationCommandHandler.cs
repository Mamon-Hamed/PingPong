using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities.Notifications;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Notifications.CreateGeneral;

public sealed class CreateGeneralNotificationCommandHandler(
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateGeneralNotificationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateGeneralNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = NotificationEntity.Create(
            request.Title,
            request.Message,
            NotificationType.General,
            request.ImageUrl);

        notificationRepository.Add(notification);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(notification.Id.Value);
    }
}
