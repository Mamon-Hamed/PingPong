using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Authentication;
using PingPong.Domain.Constants;

namespace PingPong.Infrastructure.Identity;

public sealed class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
{
    public async Task<IdentityRegistrationResponse> RegisterAdminAsync(AdminRegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            CountryId = request.CountryId,
            CityId = request.CityId
        };

        if (!string.IsNullOrEmpty(request.Address))
        {
            user.Locations.Add(new UserLocation
            {
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            });
        }

        if (!string.IsNullOrEmpty(request.DeviceId))
        {
            user.RegisteredDevices.Add(new RegisteredDevice
            {
                DeviceId = request.DeviceId,
                DeviceName = request.DeviceName ?? "Unknown Device",
                DeviceType = request.DeviceType,
                OperatingSystem = request.OperatingSystem,
                LastUsedAtUtc = DateTime.UtcNow
            });
        }

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, Roles.Admin);
        }

        return result.Succeeded
            ? new IdentityRegistrationResponse(true, user.Id, Array.Empty<string>())
            : new IdentityRegistrationResponse(false, string.Empty, result.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<IdentityRegistrationResponse> RegisterUserAsync(UserRegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            CountryId = request.CountryId,
            CityId = request.CityId
        };

        if (!string.IsNullOrEmpty(request.Address))
        {
            user.Locations.Add(new UserLocation
            {
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            });
        }

        if (!string.IsNullOrEmpty(request.DeviceId))
        {
            user.RegisteredDevices.Add(new RegisteredDevice
            {
                DeviceId = request.DeviceId,
                DeviceName = request.DeviceName ?? "Unknown Device",
                DeviceType = request.DeviceType,
                OperatingSystem = request.OperatingSystem,
                LastUsedAtUtc = DateTime.UtcNow
            });
        }

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, Roles.User);
        }

        return result.Succeeded
            ? new IdentityRegistrationResponse(true, user.Id, Array.Empty<string>())
            : new IdentityRegistrationResponse(false, string.Empty, result.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<IdentityValidationResponse> ValidateCredentialsAsync(ValidateCredentialsRequest request)
    {
        var user = await userManager.Users
            .Include(u => u.Locations)
            .Include(u => u.RegisteredDevices)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user is null || !user.IsActive)
            return new IdentityValidationResponse(false, string.Empty, string.Empty, string.Empty, []);

        var isValidPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isValidPassword)
            return new IdentityValidationResponse(false, string.Empty, string.Empty, string.Empty, []);

        if (!string.IsNullOrEmpty(request.Address))
        {
            user.Locations.Add(new UserLocation
            {
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            });
        }

        if (!string.IsNullOrEmpty(request.DeviceId))
        {
            var device = user.RegisteredDevices.FirstOrDefault(d => d.DeviceId == request.DeviceId);
            if (device != null)
            {
                device.LastUsedAtUtc = DateTime.UtcNow;
                device.DeviceName = request.DeviceName ?? device.DeviceName;
                device.DeviceType = request.DeviceType ?? device.DeviceType;
                device.OperatingSystem = request.OperatingSystem ?? device.OperatingSystem;
            }
            else
            {
                user.RegisteredDevices.Add(new RegisteredDevice
                {
                    DeviceId = request.DeviceId,
                    DeviceName = request.DeviceName ?? "Unknown Device",
                    DeviceType = request.DeviceType,
                    OperatingSystem = request.OperatingSystem,
                    LastUsedAtUtc = DateTime.UtcNow
                });
            }
        }

        user.CountryId = request.CountryId ?? user.CountryId;
        user.CityId = request.CityId ?? user.CityId;

        await userManager.UpdateAsync(user);

        var roles = await userManager.GetRolesAsync(user);

        return new IdentityValidationResponse(true, user.Id, user.Email!, user.UserName!, roles);
    }

    public async Task UpdateLastLoginAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            user.LastLoginUtc = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
        }
    }

    public async Task<bool> ValidateRefreshTokenAsync(ValidateRefreshTokenRequest request)
    {
        var user = await userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user is null || !user.IsActive)
            return false;

        var token = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken);

        if (token is null || !token.IsActive)
            return false;

        return true;
    }

    public async Task UpdateRefreshTokenAsync(UpdateRefreshTokenRequest request)
    {
        var user = await userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user is null)
            return;

        // Revoke active tokens
        var activeTokens = user.RefreshTokens.Where(rt => rt.IsActive);
        foreach (var activeToken in activeTokens)
        {
            activeToken.RevokedAtUtc = DateTime.UtcNow;
        }

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = request.NewRefreshToken,
            ExpiresAtUtc = request.ExpiresAtUtc,
            UserId = request.UserId
        });

        await userManager.UpdateAsync(user);
    }
}
