using PingPong.Domain.Entities.Notifications;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

public interface INotificationRepository : IRepository<NotificationEntity, NotificationId>
{
    Task<List<NotificationEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
