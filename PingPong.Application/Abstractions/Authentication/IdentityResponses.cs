namespace PingPong.Application.Abstractions.Authentication;

public sealed record IdentityRegistrationResponse(bool Succeeded, string UserId, string[] Errors);

public sealed record IdentityValidationResponse(
    bool Succeeded,
    string UserId,
    string Email,
    string FullName,
    IList<string> Roles);