using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Notifications.CreateGeneral;

public sealed record CreateGeneralNotificationCommand(
    string Title,
    string Message,
    string? ImageUrl) : ICommand<Guid>;
