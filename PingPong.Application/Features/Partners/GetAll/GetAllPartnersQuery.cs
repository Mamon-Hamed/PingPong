using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Entities.Categories;

namespace PingPong.Application.Features.Partners.GetAll;

public sealed record GetAllPartnersQuery : GetAllQuery<PartnerDetailsResponse>
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public Guid? CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }
    public bool? IsVerified { get; set; }
    public SubscriptionStatus? SubscriptionStatus { get; set; }
}
