using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Categories.Update;

using Abstractions.Messaging;
using Domain.Repositories;
using Common;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.Id);
        var category = await categoryRepository.GetByIdAsync(categoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        category.Update(request.Name, request.IconUrl);

        categoryRepository.Update(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
