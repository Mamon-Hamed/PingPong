using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.Create;

using Abstractions.Messaging;
using Domain.Entities;
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
        var cityId = new CityId(request.CityId);
        
        var partner = PartnerEntity.Create(
            request.CompanyName,
            request.ContactFirstName,
            request.ContactLastName,
            request.Phone,
            request.Email,
            cityId,
            categoryId,
            request.Photos,
            request.IsVerified,
            request.SubscriptionStatus);

        partnerRepository.Add(partner);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(partner.Id.Value);
    }
}
