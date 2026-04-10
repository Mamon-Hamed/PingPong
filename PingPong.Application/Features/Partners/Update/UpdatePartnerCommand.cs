namespace PingPong.Application.Features.Partners.Update;

using Abstractions.Messaging;
using Domain.Entities;

public sealed record UpdatePartnerCommand(
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
    SubscriptionStatus SubscriptionStatus) : ICommand;
