using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Countries;

public sealed class DeleteCountryCommandHandler(
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork)
    : DeleteCommandHandler<DeleteCountryCommand, CountryEntity, CountryId>(countryRepository, unitOfWork);
