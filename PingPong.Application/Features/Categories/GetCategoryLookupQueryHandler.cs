using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Categories;

public sealed class GetCategoryLookupQueryHandler(ICategoryRepository repository)
    : IQueryHandler<GetCategoryLookupQuery, List<CategoryResponse>>
{
    public async Task<Result<List<CategoryResponse>>> Handle(GetCategoryLookupQuery request, CancellationToken cancellationToken)
    {
        var lookup = await repository.GetAsNoTrackingAsync()
            .Select(c => new CategoryResponse(c.Id.Value, c.Name, c.IconUrl, c.Color))
            .ToListAsync(cancellationToken);

        return Result.Success(lookup);
    }
}
