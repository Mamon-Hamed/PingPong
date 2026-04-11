using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.GetById;

public sealed class GetPartnerByIdQueryHandler(IPartnerRepository partnerRepository)
    : GetByIdQueryHandler<GetPartnerByIdQuery, PartnerEntity, PartnerId, PartnerResponse>(partnerRepository)
{
    protected override PartnerResponse MapToResponse(PartnerEntity entity)
    {
        return new PartnerResponse(
            entity.Id.Value,
            entity.CompanyName,
            entity.ContactFirstName,
            entity.ContactLastName,
            entity.Phone,
            entity.Email,
            entity.City,
            entity.CategoryId.Value,
            entity.Photos,
            entity.IsVerified,
            entity.SubscriptionStatus);
    }
}
