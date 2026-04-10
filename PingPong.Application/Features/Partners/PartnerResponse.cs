namespace PingPong.Application.Features.Partners;

using Domain.Entities;

public record PartnerResponse(
    Guid Id,
    string CompanyName,
    string ContactFirstName,
    string ContactLastName,
    string Phone,
    string Email,
    string City,
    Guid CategoryId,
    List<string> Photos,
    bool IsVerified,
    SubscriptionStatus SubscriptionStatus);
