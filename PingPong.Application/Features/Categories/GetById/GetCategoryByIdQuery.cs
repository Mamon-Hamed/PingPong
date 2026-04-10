using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Categories.GetById;

public sealed record GetCategoryByIdQuery(Guid Id) : GetByIdQuery<CategoryId, CategoryResponse>(Id);
