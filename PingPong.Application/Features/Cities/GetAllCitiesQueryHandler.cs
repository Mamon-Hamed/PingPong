using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Entities;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed class GetAllCitiesQueryHandler(ICityRepository repository)
    : GetAllQueryHandler<GetAllCitiesQuery, CityEntity, CityId, CityResponse>(repository)
{
    protected override IQueryable<CityEntity> BuildQuery(GetAllCitiesQuery query)
    {
        return Queryable.FilterBase(query)
            .WhereIf(!string.IsNullOrEmpty(query.Name), c => c.Name.Contains(query.Name!))
            .WhereIf(query.CountryId.HasValue, c => c.CountryId == new CountryId(query.CountryId!.Value));
    }
}
