using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Application.Shared;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed class GetCityLookupQueryHandler(ICityRepository repository)
    : IQueryHandler<GetCityLookupQuery, List<LookupResponse>>
{
    public async Task<Result<List<LookupResponse>>> Handle(GetCityLookupQuery request, CancellationToken cancellationToken)
    {
        var lookup = await repository.GetAsNoTrackingAsync()
            .WhereIf(request.CountryId.HasValue, c => c.CountryId == new CountryId(request.CountryId!.Value))
            .Select(c => new LookupResponse(c.Id.Value, c.Name))
            .ToListAsync(cancellationToken);

        return Result.Success(lookup);
    }
}
