using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Models;

namespace PingPong.Application.Features.Partners.Create;

using Abstractions.Messaging;
using PingPong.Domain.Entities.Partners;
using Domain.Repositories;
using Common;

public sealed class CreatePartnerCommandHandler(
    IPartnerRepository partnerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePartnerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.CategoryId);
        var countryId = new CountryId(request.CountryId);
        var cityId = new CityId(request.CityId);
        var location = new Location(
            request.Location.Latitude,
            request.Location.Longitude,
            request.Location.City,
            request.Location.Country,
            request.Location.Address);
        
        var partner = PartnerEntity.Create(
            request.Name,
            request.Phone,
            request.MediaUrl,
            request.ValidUntil,
            request.Gallery,
            categoryId,
            countryId,
            cityId,
            location,
            request.SubscriptionStatus);

        partnerRepository.Add(partner);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(partner.Id.Value);
    }
}
