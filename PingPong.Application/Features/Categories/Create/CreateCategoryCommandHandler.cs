namespace PingPong.Application.Features.Categories.Create;

using Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Common;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = CategoryEntity.Create(request.Name, request.IconUrl);

        categoryRepository.Add(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(category.Id.Value);
    }
}
