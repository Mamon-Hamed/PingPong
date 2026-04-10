using System.Linq.Expressions;
using PingPong.Domain.Models;
using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

public interface IRepository<TEntity, in TId>
    where TEntity : Entity<TId>
    where TId : StronglyTypedId
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PaginatedList<TDestination>> GetPaginatedAsync<TDestination, TQuery>(
        TQuery request,
        Func<TQuery, IQueryable<TEntity>> query,
        CancellationToken cancellationToken = default)
        where TQuery : IAuditQuery;
    Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);
    IQueryable<TEntity> GetAsNoTrackingAsync();
}
