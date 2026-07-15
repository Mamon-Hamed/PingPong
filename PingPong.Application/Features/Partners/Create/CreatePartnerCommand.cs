namespace PingPong.Application.Features.Partners.Create;

using Abstractions.Messaging;
using Domain.Entities;

public sealed record CreatePartnerCommand(
    string CompanyName,
    string ContactFirstName,
    string ContactLastName,
    string Phone,
    string Email,
    Guid CityId,
    Guid CategoryId,
    List<string> Photos,
    bool IsVerified,
    SubscriptionStatus SubscriptionStatus) : ICommand<Guid>;
