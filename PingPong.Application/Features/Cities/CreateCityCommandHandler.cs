using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed class CreateCityCommandHandler(
    ICityRepository cityRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCityCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = CityEntity.Create(request.Name, new CountryId(request.CountryId));

        cityRepository.Add(city);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(city.Id.Value);
    }
}
