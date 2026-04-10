using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.Update;

using Abstractions.Messaging;
using Domain.Repositories;
using Common;

internal sealed class UpdatePartnerCommandHandler(
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

        partner.Update(
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

        partnerRepository.Update(partner);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
