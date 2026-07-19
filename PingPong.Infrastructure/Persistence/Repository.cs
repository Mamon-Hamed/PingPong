using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Models;
using PingPong.Domain.Primitives;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence;

public abstract class Repository<TEntity, TId>(AppDbContext dbContext) : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : StronglyTypedId
{
    protected readonly AppDbContext DbContext = dbContext;
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return DbSet.ToListAsync(cancellationToken);
    }

    public Task<PaginatedList<TDestination>> GetPaginatedAsync<TDestination, TQuery>(
        TQuery request,
        Func<TQuery, IQueryable<TEntity>> query,
        CancellationToken cancellationToken = default)
        where TQuery : IAuditQuery
    {
        return query(request)
                .AsNoTracking()
                .ProjectToType<TDestination>(TypeAdapterConfig.GlobalSettings)
                .PaginatedListAsync(request.Page, request.PageSize, cancellationToken)
            ;
    }

    public IQueryable<TEntity> GetAsNoTrackingAsync()
    {
        return DbSet.AsNoTracking().AsQueryable();
    }

    public async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }

    public void Add(TEntity entity) => DbSet.Add(entity);

    public void Update(TEntity entity) => DbSet.Update(entity);

    public void Remove(TEntity entity) => DbSet.Remove(entity);
}