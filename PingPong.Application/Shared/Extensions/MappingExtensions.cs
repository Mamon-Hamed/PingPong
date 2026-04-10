#region

using Mapster;
using Microsoft.EntityFrameworkCore;
using PingPong.Domain.Models;

#endregion

namespace PingPong.Application.Shared.Extensions;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>
    (this IQueryable<TDestination> queryable, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        static async Task<PaginatedList<TDestination>> CreateAsync(IQueryable<TDestination> source, int pageNumber, int pageSize,
            CancellationToken cancellation = default)
        {
            var count = await source.CountAsync(cancellation);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellation)
                .ConfigureAwait(false);

            return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
        }
        return CreateAsync(queryable, pageNumber, pageSize, cancellationToken);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable,CancellationToken cancellationToken = default)
    {
        return queryable.ProjectToType<TDestination>().ToListAsync(cancellationToken);
    }
}