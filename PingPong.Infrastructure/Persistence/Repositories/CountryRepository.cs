using PingPong.Domain.Entities;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Repositories;

internal sealed class CountryRepository(AppDbContext dbContext)
    : Repository<CountryEntity, CountryId>(dbContext), ICountryRepository
{
}
