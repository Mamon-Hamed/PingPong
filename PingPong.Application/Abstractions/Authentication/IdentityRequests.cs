namespace PingPong.Application.Abstractions.Authentication;

public sealed record AdminRegisterRequest(
    string UserName, 
    string Email, 
    string Password,
    string? Address = null,
    double? Latitude = null,
    double? Longitude = null,
    string? DeviceId = null,
    string? DeviceName = null,
    string? DeviceType = null,
    string? OperatingSystem = null,
    Guid? CountryId = null,
    Guid? CityId = null);

public sealed record UserRegisterRequest(
    string UserName, 
    string Email, 
    string PhoneNumber, 
    string Password,
    string? Address = null,
    double? Latitude = null,
    double? Longitude = null,
    string? DeviceId = null,
    string? DeviceName = null,
    string? DeviceType = null,
    string? OperatingSystem = null,
    Guid? CountryId = null,
    Guid? CityId = null,
    string? AvatarUrl = null);

public sealed record ValidateCredentialsRequest(
    string Email, 
    string Password,
    string? Address = null,
    double? Latitude = null,
    double? Longitude = null,
    string? DeviceId = null,
    string? DeviceName = null,
    string? DeviceType = null,
    string? OperatingSystem = null,
    Guid? CountryId = null,
    Guid? CityId = null);

public sealed record ValidateRefreshTokenRequest(string UserId, string RefreshToken);

public sealed record UpdateRefreshTokenRequest(string UserId, string NewRefreshToken, DateTime ExpiresAtUtc);

public sealed record UpdateUserIdentityRequest(
    string Id,
    string UserName,
    string Email,
    string? PhoneNumber,
    string? AvatarUrl,
    bool IsActive,
    Guid? CountryId,
    Guid? CityId);
