using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Repositories;

using Domain.Entities;
using PingPong.Domain.Repositories;

internal sealed class CategoryRepository(AppDbContext dbContext)
    : Repository<CategoryEntity, CategoryId>(dbContext), ICategoryRepository
{
}
