using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Countries;

public sealed class GetCountryByIdQueryHandler(ICountryRepository repository)
    : GetByIdQueryHandler<GetCountryByIdQuery, CountryEntity, CountryId, CountryResponse>(repository)
{
    protected override CountryResponse MapToResponse(CountryEntity entity)
    {
        return new CountryResponse(entity.Id.Value, entity.Name, entity.Code);
    }
}
