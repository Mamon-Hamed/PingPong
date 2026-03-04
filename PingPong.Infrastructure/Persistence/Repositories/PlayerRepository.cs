using Microsoft.EntityFrameworkCore;
using PingPong.Domain.Entities;
using PingPong.Domain.Repositories;

namespace PingPong.Infrastructure.Persistence.Repositories;

public sealed class PlayerRepository(AppDbContext dbContext)
    : Repository<Player, PlayerId>(dbContext), IPlayerRepository
{
    public async Task<Player?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
    }
}
