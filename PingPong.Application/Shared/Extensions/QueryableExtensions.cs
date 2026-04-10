#region

using System.Linq.Expressions;
using PingPong.Domain.Primitives;
using System.Linq.Dynamic.Core;
using PingPong.Domain.Repositories;

#endregion

namespace PingPong.Application.Shared.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> FilterBase<T>(this IQueryable<T> query, IAuditQuery filter) where T : IAuditableEntity
    {
        return query
                .WhereIf(filter.MinCreatedAt.HasValue, x => x.CreatedAt >= filter.MinCreatedAt)
                .WhereIf(filter.MaxCreatedAt.HasValue, x => x.CreatedAt <= filter.MaxCreatedAt)
                .WhereIf(filter.MinUpdatedAt.HasValue, x => x.UpdatedAt >= filter.MinUpdatedAt)
                .WhereIf(filter.MaxUpdatedAt.HasValue, x => x.UpdatedAt <= filter.MaxUpdatedAt)
                .WhereIf(!string.IsNullOrEmpty(filter.CreatedBy), x => x.CreatedBy!.Contains(filter.CreatedBy!))
                .WhereIf(!string.IsNullOrEmpty(filter.UpdatedBy), x => x.UpdatedBy!.Contains(filter.UpdatedBy!))
                .WhereIf(!string.IsNullOrEmpty(filter.CreatedByName),x => x.CreatedByName!.Contains(filter.CreatedByName!))
                .WhereIf(!string.IsNullOrEmpty(filter.UpdatedByName),x => x.UpdatedByName!.Contains(filter.UpdatedByName!))
                .OrderBase(filter)
            ;
       
    }

    public static IQueryable<T> OrderBase<T>(this IQueryable<T> query, ISortQuery filter)
    {
        return filter.IsAscending
            ? query.OrderIf(!string.IsNullOrEmpty(filter.SortBy), filter.SortBy!)
            : query.OrderByDescendingIf(!string.IsNullOrEmpty(filter.SortBy), filter.SortBy!);
    }

    
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
        Expression<Func<T, bool>> predicate)
    {
        if (condition) return query.Where(predicate);
        return query;
    }


    public static IQueryable<T> OrderIf<T>(this IQueryable<T> query,
        bool condition,
        string orderKey
    )
    {
        if (condition) return query.OrderBy(orderKey);
        return query;
    }

    public static IQueryable<T> OrderByDescendingIf<T>(this IQueryable<T> query,
        bool condition,
        string orderByKey
    )
    {
        if (condition) return query.OrderBy(orderByKey + " desc");
        return query;
    }

    public static IQueryable<T> ApplyOrdering<T, TKey>(this IQueryable<T> query,
        Dictionary<string, Expression<Func<T, TKey>>> allowedKeys, string? orderValue, bool? isDescending)
    {
        if (orderValue is null)
            return query;

        var success = allowedKeys.TryGetValue(orderValue, out var keySelector);
        if (!success) throw new ApplicationException("invalid order value");
        query = isDescending switch
        {
            null => query.OrderBy(keySelector!),
            false => query.OrderBy(keySelector!),
            true => query.OrderByDescending(keySelector!)
        };
        return query;
    }
}