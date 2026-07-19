using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Models;

namespace PingPong.Application.Features.Partners.Update;

using Abstractions.Messaging;
using Domain.Repositories;
using Common;

public sealed class UpdatePartnerCommandHandler(
    IPartnerRepository partnerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePartnerCommand>
{
    public async Task<Result> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
    {
        var partnerId = new PartnerId(request.Id);
        var partner = await partnerRepository.GetByIdAsync(partnerId, cancellationToken);

        if (partner is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        var categoryId = new CategoryId(request.CategoryId);
        var countryId = new CountryId(request.CountryId);
        var cityId = new CityId(request.CityId);
        var location = new Location(
            request.Location.Latitude,
            request.Location.Longitude,
            request.Location.City,
            request.Location.Country,
            request.Location.Address);

        partner.Update(
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

        partnerRepository.Update(partner);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
