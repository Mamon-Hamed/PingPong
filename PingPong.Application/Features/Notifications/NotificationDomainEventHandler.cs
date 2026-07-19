using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Notifications;
using PingPong.Domain.Events;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Notifications;

public sealed class NotificationDomainEventHandler(
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork)
    : IDomainEventHandler<PartnerCreatedDomainEvent>,
      IDomainEventHandler<PartnerServiceUpdatedDomainEvent>,
      IDomainEventHandler<GeneralNotificationAddedDomainEvent>
{
    public async Task Handle(PartnerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var notificationEntity = NotificationEntity.Create(
            "New Partner Joined!",
            $"Welcome {notification.Name} to our platform.",
            NotificationType.PartnerAdded,
            notification.MediaUrl);

        notificationRepository.Add(notificationEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(PartnerServiceUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var notificationEntity = NotificationEntity.Create(
            "Service Offer Updated!",
            $"{notification.PartnerName} has updated their service: {notification.ServiceName}.",
            NotificationType.ServiceUpdated);

        notificationRepository.Add(notificationEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(GeneralNotificationAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        var notificationEntity = NotificationEntity.Create(
            notification.Title,
            notification.Message,
            NotificationType.General,
            notification.ImageUrl);

        notificationRepository.Add(notificationEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
