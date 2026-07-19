using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities.Categories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Categories.GetById;

public sealed class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : GetByIdQueryHandler<GetCategoryByIdQuery, CategoryEntity, CategoryId, CategoryResponse>(categoryRepository)
{
    protected override CategoryResponse MapToResponse(CategoryEntity entity)
    {
        return new CategoryResponse(entity.Id.Value, entity.Name, entity.IconUrl, entity.Color);
    }
}
