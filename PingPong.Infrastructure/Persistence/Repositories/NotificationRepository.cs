using PingPong.Domain.Entities.Notifications;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

using Microsoft.EntityFrameworkCore;

namespace PingPong.Infrastructure.Persistence.Repositories;

internal sealed class NotificationRepository(AppDbContext dbContext)
    : Repository<NotificationEntity, NotificationId>(dbContext), INotificationRepository
{
    public async Task<List<NotificationEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Notifications
            .AsNoTracking()
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
