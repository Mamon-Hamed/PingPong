namespace PingPong.Application.Features.Notifications;

public sealed record NotificationResponse(
    Guid Id,
    string Title,
    string Message,
    string? ImageUrl,
    string Type,
    DateTime CreatedAt);
