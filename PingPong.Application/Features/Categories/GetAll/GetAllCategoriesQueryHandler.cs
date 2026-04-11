using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Categories.GetAll;

public sealed class GetAllCategoriesQueryHandler(ICategoryRepository repository)
    : GetAllQueryHandler<GetAllCategoriesQuery, CategoryEntity, CategoryId, CategoryResponse>(repository)
{
    protected override IQueryable<CategoryEntity> BuildQuery(GetAllCategoriesQuery query)
    {
        return Queryable.FilterBase(query)
            .WhereIf(!string.IsNullOrEmpty(query.Name), c => c.Name.Contains(query.Name!));
    }
}
