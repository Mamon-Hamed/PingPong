using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.GetById;

public sealed record GetPartnerByIdQuery(
    Guid Id
) : GetByIdQuery<PartnerId, PartnerDetailsResponse>(Id)
{
    public double UserLatitude { get; init; }
    public double UserLongitude { get; init; }
}