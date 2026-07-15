using Microsoft.AspNetCore.Identity;

namespace PingPong.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginUtc { get; set; }
    public bool IsActive { get; set; } = true;
    
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public List<UserLocation> Locations { get; set; } = [];
    public List<RegisteredDevice> RegisteredDevices { get; set; } = [];
    
    public Guid? CountryId { get; set; }
    public Guid? CityId { get; set; }
}

public class UserLocation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Address { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}

public class RegisteredDevice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string DeviceId { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public string? DeviceType { get; set; }
    public string? OperatingSystem { get; set; }
    public DateTime RegisteredAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? LastUsedAtUtc { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedAtUtc { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;
    public bool IsActive => RevokedAtUtc is null && !IsExpired;
    
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
