using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Notifications;

public sealed class NotificationEntity : Entity<NotificationId>
{
    private NotificationEntity(
        NotificationId id,
        string title,
        string message,
        string? imageUrl,
        NotificationType type,
        DateTime createdAtUtc)
        : base(id)
    {
        Title = title;
        Message = message;
        ImageUrl = imageUrl;
        Type = type;
        CreatedAt = createdAtUtc;
    }

    private NotificationEntity() { } // EF Core

    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public NotificationType Type { get; private set; }

    public static NotificationEntity Create(
        string title,
        string message,
        NotificationType type,
        string? imageUrl = null)
    {
        return new NotificationEntity(
            NotificationId.New(),
            title,
            message,
            imageUrl,
            type,
            DateTime.UtcNow);
    }
}

public enum NotificationType
{
    General = 0,
    PartnerAdded = 1,
    ServiceUpdated = 2
}
