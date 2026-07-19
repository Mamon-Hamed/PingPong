using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Notifications.GetAll;

public sealed record GetAllNotificationsQuery : IQuery<List<NotificationResponse>>;
