using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Notifications.GetAll;

public sealed class GetAllNotificationsQueryHandler(INotificationRepository notificationRepository)
    : IQueryHandler<GetAllNotificationsQuery, List<NotificationResponse>>
{
    public async Task<Result<List<NotificationResponse>>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await notificationRepository.GetAllAsync(cancellationToken);

        var response = notifications.Select(n => new NotificationResponse(
            n.Id.Value,
            n.Title,
            n.Message,
            n.ImageUrl,
            n.Type.ToString(),
            n.CreatedAt))
            .ToList();

        return Result.Success(response);
    }
}
