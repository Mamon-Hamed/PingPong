using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Application.Shared;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Countries;

public sealed class GetCountryLookupQueryHandler(ICountryRepository repository)
    : IQueryHandler<GetCountryLookupQuery, List<LookupResponse>>
{
    public async Task<Result<List<LookupResponse>>> Handle(GetCountryLookupQuery request, CancellationToken cancellationToken)
    {
        var lookup = await repository.GetAsNoTrackingAsync()
            .Select(c => new LookupResponse(c.Id.Value, c.Name))
            .ToListAsync(cancellationToken);

        return Result.Success(lookup);
    }
}
