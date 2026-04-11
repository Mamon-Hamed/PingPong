using PingPong.Application.Common;
using PingPong.Domain.Models;
using PingPong.Domain.Repositories;
using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Abstractions.Messaging;

public abstract class GetByIdQueryHandler<TQuery, TEntity, TId, TResponse>(
    IRepository<TEntity, TId> repository)
    : IQueryHandler<TQuery, TResponse>
    where TQuery : GetByIdQuery<TId, TResponse>
    where TEntity : Entity<TId>
    where TId : StronglyTypedId
{
    public virtual async Task<Result<TResponse>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var id = (TId)Activator.CreateInstance(typeof(TId), request.Id)!;
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<TResponse>("The requested entity was not found.");
        }

        return Result.Success(MapToResponse(entity));
    }

    protected abstract TResponse MapToResponse(TEntity entity);
}

public abstract class GetAllQueryHandler<TQuery, TEntity, TId, TResponse>(
    IRepository<TEntity, TId> repository)
    : IQueryHandler<TQuery, PaginatedList<TResponse>>
    where TQuery : GetAllQuery<TResponse>
    where TEntity : Entity<TId>
    where TId : StronglyTypedId
{
    protected IQueryable<TEntity> Queryable => repository.GetAsNoTrackingAsync();
    public virtual async Task<Result<PaginatedList<TResponse>>> Handle(TQuery request,
        CancellationToken cancellationToken)
    {
        var items = await repository
            .GetPaginatedAsync<TResponse,TQuery>(request,BuildQuery, cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(items);
    }

    protected abstract IQueryable<TEntity> BuildQuery(TQuery query);
}

public abstract class DeleteCommandHandler<TCommand, TEntity, TId>(
    IRepository<TEntity, TId> repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<TCommand>
    where TCommand : DeleteCommand<TId>
    where TEntity : Entity<TId>
    where TId : StronglyTypedId
{
    public virtual async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var id = (TId)Activator.CreateInstance(typeof(TId), request.Id)!;
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        repository.Remove(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}