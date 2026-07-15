using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Auth.Register;

public sealed record AdminRegisterCommand(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword,
    string? Address = null,
    double? Latitude = null,
    double? Longitude = null,
    string? DeviceId = null,
    string? DeviceName = null,
    string? DeviceType = null,
    string? OperatingSystem = null,
    Guid? CountryId = null,
    Guid? CityId = null) : ICommand;
