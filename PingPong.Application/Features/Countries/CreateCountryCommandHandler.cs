using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Countries;

public sealed class CreateCountryCommandHandler(
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCountryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = CountryEntity.Create(request.Name, request.Code);

        countryRepository.Add(country);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(country.Id.Value);
    }
}
