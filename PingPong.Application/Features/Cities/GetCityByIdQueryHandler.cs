using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed class GetCityByIdQueryHandler(ICityRepository repository)
    : GetByIdQueryHandler<GetCityByIdQuery, CityEntity, CityId, CityResponse>(repository)
{
    protected override CityResponse MapToResponse(CityEntity entity)
    {
        return new CityResponse(entity.Id.Value, entity.Name, entity.CountryId.Value);
    }
}
