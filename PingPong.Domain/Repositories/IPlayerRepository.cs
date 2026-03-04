using PingPong.Domain.Entities;

namespace PingPong.Domain.Repositories;

public interface IPlayerRepository : IRepository<Player, PlayerId>
{
    Task<Player?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
