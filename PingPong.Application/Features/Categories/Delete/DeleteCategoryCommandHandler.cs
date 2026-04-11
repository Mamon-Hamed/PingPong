using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Categories.Delete;

public sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : DeleteCommandHandler<DeleteCategoryCommand, CategoryEntity, CategoryId>(categoryRepository, unitOfWork);
