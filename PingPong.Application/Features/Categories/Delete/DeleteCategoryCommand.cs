using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Categories.Delete;

public sealed record DeleteCategoryCommand(Guid Id) : DeleteCommand<CategoryId>(Id);
