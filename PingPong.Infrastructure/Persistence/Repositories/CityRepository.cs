using PingPong.Domain.Entities;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Repositories;

internal sealed class CityRepository(AppDbContext dbContext)
    : Repository<CityEntity, CityId>(dbContext), ICityRepository
{
}
