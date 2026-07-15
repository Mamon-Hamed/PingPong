using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed class UpdateCityCommandHandler(
    ICityRepository cityRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCityCommand>
{
    public async Task<Result> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var cityId = new CityId(request.Id);
        var city = await cityRepository.GetByIdAsync(cityId, cancellationToken);

        if (city is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        city.Update(request.Name, new CountryId(request.CountryId));

        cityRepository.Update(city);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
