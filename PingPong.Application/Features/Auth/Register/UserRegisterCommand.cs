using PingPong.Application.Abstractions.Messaging;

using PingPong.Application.Features.Auth.Login;

namespace PingPong.Application.Features.Auth.Register;

public sealed record UserRegisterCommand(
    string UserName,
    string Email,
    string PhoneNumber,
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
    Guid? CityId = null,
    string? AvatarUrl = null) : ICommand<LoginResponse>;
