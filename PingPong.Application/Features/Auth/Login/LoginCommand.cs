
using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Auth.Login;

public sealed record LoginCommand(
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
    Guid? CityId = null) : ICommand<LoginResponse>;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAtUtc);
