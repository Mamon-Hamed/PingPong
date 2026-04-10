using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Repositories;

using Domain.Entities;
using PingPong.Domain.Repositories;

internal sealed class PartnerRepository(AppDbContext dbContext)
    : Repository<PartnerEntity, PartnerId>(dbContext), IPartnerRepository
{
}
