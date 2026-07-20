using Microsoft.EntityFrameworkCore;
using Mapster;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;
using PingPong.Application.Common;
using PingPong.Application.Abstractions.Authentication;
using Cortex.Mediator;
using PingPong.Domain.Events;

namespace PingPong.Application.Features.Partners.GetById;

public sealed class GetPartnerByIdQueryHandler(
    IPartnerRepository partnerRepository,
    ICurrentUserService currentUser,
    IMediator mediator)
    : IQueryHandler<GetPartnerByIdQuery, PartnerDetailsResponse>
{
    public async Task<Result<PartnerDetailsResponse>> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
    {
        var partnerId = new PartnerId(request.Id);
        var partner = await partnerRepository.GetAsNoTrackingAsync()
            .Include(p => p.Services)
                .ThenInclude(s => s.Currency)
            .Include(p => p.Reviews)
            .Include(p => p.OpeningHours)
            .Where(p => p.Id == partnerId)
            .ProjectToType<PartnerDetailsResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        if (partner is null)
        {
            return Result.Failure<PartnerDetailsResponse>("The requested entity was not found.");
        }

        // var isFavorite = currentUser.FavoritePartnerIds.Contains(partner.Id);
        // partner = partner with { IsFavorite = isFavorite };

      await Task.Run(async () =>
        {
            await mediator.PublishAsync(new PartnerViewedDomainEvent(partnerId), cancellationToken);
        }, cancellationToken);

        return Result.Success(partner with{Views = partner.Views + 1});
    }
}
