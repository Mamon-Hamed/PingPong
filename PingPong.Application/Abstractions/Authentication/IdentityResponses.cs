namespace PingPong.Application.Abstractions.Authentication;

public sealed record IdentityRegistrationResponse(bool Succeeded, string UserId, string[] Errors);

public sealed record IdentityValidationResponse(
    bool Succeeded,
    string UserId,
    string Email,
    string UserName,
    IList<string> Roles);

public sealed record UserIdentityResponse(
    string Id,
    string UserName,
    string Email,
    string? PhoneNumber,
    string? AvatarUrl,
    bool IsActive,
    DateTime CreatedAtUtc,
    DateTime? LastLoginUtc,
    Guid? CountryId,
    Guid? CityId);