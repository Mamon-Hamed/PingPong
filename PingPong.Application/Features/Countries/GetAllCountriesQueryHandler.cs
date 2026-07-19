using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Countries;

public sealed class GetAllCountriesQueryHandler(ICountryRepository repository)
    : GetAllQueryHandler<GetAllCountriesQuery, CountryEntity, CountryId, CountryResponse>(repository)
{
    protected override IQueryable<CountryEntity> BuildQuery(GetAllCountriesQuery query)
    {
        return Queryable.FilterBase(query)
            .WhereIf(!string.IsNullOrEmpty(query.Name), c => c.Name.Contains(query.Name!));
    }
}
