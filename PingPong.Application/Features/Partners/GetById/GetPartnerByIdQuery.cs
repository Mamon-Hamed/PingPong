using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.GetById;

public sealed record GetPartnerByIdQuery(
    Guid Id,
    double UserLatitude,
    double UserLongitude) : GetByIdQuery<PartnerId, PartnerResponse>(Id);
