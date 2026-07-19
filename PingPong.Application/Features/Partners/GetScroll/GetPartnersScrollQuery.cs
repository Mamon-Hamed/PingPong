using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Models;

namespace PingPong.Application.Features.Partners.GetScroll;

public sealed record GetPartnersScrollQuery : IQuery<PaginatedList<PartnerResponse>>
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public string? Name { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public Guid? CategoryId { get; init; }
    public bool? IsVerified { get; init; }
    public SubscriptionStatus? SubscriptionStatus { get; init; }
    
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 10;
}
