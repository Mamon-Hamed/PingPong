using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Countries;

public sealed class UpdateCountryCommandHandler(
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCountryCommand>
{
    public async Task<Result> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var countryId = new CountryId(request.Id);
        var country = await countryRepository.GetByIdAsync(countryId, cancellationToken);

        if (country is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        country.Update(request.Name, request.Code);

        countryRepository.Update(country);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
