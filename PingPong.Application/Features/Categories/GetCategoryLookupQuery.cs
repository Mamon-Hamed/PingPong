using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Categories;

public sealed record GetCategoryLookupQuery : IQuery<List<CategoryResponse>>;
