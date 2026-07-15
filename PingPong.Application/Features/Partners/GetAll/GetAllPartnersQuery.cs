using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities;

namespace PingPong.Application.Features.Partners.GetAll;

public sealed record GetAllPartnersQuery : GetAllQuery<PartnerResponse>
{
    public string? CompanyName { get;  set; }

    public string? ContactFirstName { get;  set; }

    public string? ContactLastName { get;  set; }

    public string? Phone { get;  set; }

    public string? Email { get;  set; }

    public Guid? CityId { get;  set; }

    public Guid? CategoryId { get;  set; } 

    public CategoryEntity? Category { get;  set; }
    
    public bool? IsVerified { get;  set; }

    public SubscriptionStatus? SubscriptionStatus { get;  set; }
}
