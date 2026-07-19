namespace PingPong.Application.Features.Users;

public record UserResponse(
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
