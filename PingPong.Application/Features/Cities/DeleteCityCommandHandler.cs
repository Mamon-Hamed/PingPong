using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed class DeleteCityCommandHandler(
    ICityRepository cityRepository,
    IUnitOfWork unitOfWork)
    : DeleteCommandHandler<DeleteCityCommand, CityEntity, CityId>(cityRepository, unitOfWork);
