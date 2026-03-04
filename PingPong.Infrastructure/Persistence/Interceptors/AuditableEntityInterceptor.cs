using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PingPong.Application.Abstractions.Authentication;
using PingPong.Domain.Primitives;

namespace PingPong.Infrastructure.Persistence.Interceptors;

public sealed class AuditableEntityInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private void UpdateAuditableEntities(DbContext? context)
    {
        if (context is null) return;

        var entries = context.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(IAuditableEntity.CreatedAtUtc)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(IAuditableEntity.CreatedBy)).CurrentValue = currentUserService.UserId;
            }

            entry.Property(nameof(IAuditableEntity.UpdatedAtUtc)).CurrentValue = DateTime.UtcNow;
            entry.Property(nameof(IAuditableEntity.UpdatedBy)).CurrentValue = currentUserService.UserId;
        }
    }
}
