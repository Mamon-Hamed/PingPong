using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.Create;

using Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Common;

internal sealed class CreatePartnerCommandHandler(
    IPartnerRepository partnerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePartnerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.CategoryId);
        
        var partner = PartnerEntity.Create(
            request.CompanyName,
            request.ContactFirstName,
            request.ContactLastName,
            request.Phone,
            request.Email,
            request.City,
            categoryId,
            request.Photos,
            request.IsVerified,
            request.SubscriptionStatus);

        partnerRepository.Add(partner);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(partner.Id.Value);
    }
}
