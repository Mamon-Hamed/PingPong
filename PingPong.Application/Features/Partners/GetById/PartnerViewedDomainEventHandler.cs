using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Events;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Partners.GetById;

public sealed class PartnerViewedDomainEventHandler(
    IPartnerRepository partnerRepository)
    : IDomainEventHandler<PartnerViewedDomainEvent>
{
    public async Task Handle(PartnerViewedDomainEvent notification, CancellationToken cancellationToken)
    {
        await partnerRepository.GetQueryable()
            .Where(p => p.Id == notification.PartnerId)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.Views, p => p.Views + 1), cancellationToken);
    }
}
