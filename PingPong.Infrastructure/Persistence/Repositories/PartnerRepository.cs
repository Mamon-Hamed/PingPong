using PingPong.Domain.StronglyTypes;
using Microsoft.EntityFrameworkCore;

namespace PingPong.Infrastructure.Persistence.Repositories;

using Domain.Entities.Partners;
using PingPong.Domain.Repositories;

internal sealed class PartnerRepository(AppDbContext dbContext)
    : Repository<PartnerEntity, PartnerId>(dbContext), IPartnerRepository
{
    public override async Task<PartnerEntity?> GetByIdAsync(PartnerId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Partners
            .Include(p => p.OpeningHours)
            .Include(p => p.Services)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
