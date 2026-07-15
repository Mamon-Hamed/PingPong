using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Constants;
using PingPong.Infrastructure.Identity;

namespace PingPong.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Email)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(u => u.NormalizedEmail)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(u => u.UserName)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(u => u.NormalizedUserName)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(StringLengths.Length64);
            
        builder.Property(u => u.PasswordHash)
            .HasMaxLength(StringLengths.Length512);
            
        builder.Property(u => u.SecurityStamp)
            .HasMaxLength(StringLengths.Length512);
            
        builder.Property(u => u.ConcurrencyStamp)
            .HasMaxLength(StringLengths.Length512);

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.NormalizedUserName).IsUnique();

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Locations)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RegisteredDevices)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.CountryId)
            .IsRequired(false);

        builder.Property(u => u.CityId)
            .IsRequired(false);
    }
}

public class UserLocationConfiguration : IEntityTypeConfiguration<UserLocation>
{
    public void Configure(EntityTypeBuilder<UserLocation> builder)
    {
        builder.ToTable("UserLocations");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Address)
            .HasMaxLength(StringLengths.Length512)
            .IsRequired();

        builder.Property(l => l.UserId)
            .HasMaxLength(StringLengths.UserId)
            .IsRequired();
    }
}

public class RegisteredDeviceConfiguration : IEntityTypeConfiguration<RegisteredDevice>
{
    public void Configure(EntityTypeBuilder<RegisteredDevice> builder)
    {
        builder.ToTable("RegisteredDevices");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DeviceId)
            .HasMaxLength(StringLengths.Length256)
            .IsRequired();

        builder.Property(d => d.DeviceName)
            .HasMaxLength(StringLengths.Length256)
            .IsRequired();

        builder.Property(d => d.DeviceType)
            .HasMaxLength(StringLengths.Length128);

        builder.Property(d => d.OperatingSystem)
            .HasMaxLength(StringLengths.Length128);

        builder.Property(d => d.UserId)
            .HasMaxLength(StringLengths.UserId)
            .IsRequired();

        builder.HasIndex(d => d.DeviceId);
    }
}

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        
        builder.HasKey(rt => rt.Id);
        
        builder.Property(rt => rt.Token)
            .HasMaxLength(StringLengths.Length256)
            .IsRequired();
        builder.Property(rt => rt.UserId)
            .HasMaxLength(StringLengths.UserId)
            .IsRequired();
            
        builder.HasIndex(rt => rt.Token).IsUnique();
    }
}

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.Property(r => r.Name)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(r => r.NormalizedName)
            .HasMaxLength(StringLengths.Length256);
            
        builder.Property(r => r.ConcurrencyStamp)
            .HasMaxLength(StringLengths.Length512);
            
        builder.HasIndex(r => r.NormalizedName).IsUnique();

        builder.HasData(
            new IdentityRole()
            {
              Id  = "e1f2d3c4-5b6a-7e8f-9g0h-1i2j3k4l5m6n",
              Name = Roles.SuperAdmin,
              NormalizedName = Roles.SuperAdmin.ToUpperInvariant()
            },
            new IdentityRole
            {
                Id = "b9793138-0c65-4f24-8197-285b0d0246a1",
                Name = Roles.Admin,
                NormalizedName = Roles.Admin.ToUpperInvariant()
            },
            new IdentityRole()
            {
                Id = "f3c1e2d4-8b6a-4f5e-9c3b-1a2d3e4f5g6h",
                Name = Roles.Partner,
                NormalizedName = Roles.Partner.ToUpperInvariant()
            },
            new IdentityRole
            {
                Id = "60e86b02-5c62-4414-871d-5511b8b7e283",
                Name = Roles.User,
                NormalizedName = Roles.User.ToUpperInvariant()
            }
        );
    }
}

public class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.Property(uc => uc.ClaimType)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(uc => uc.ClaimValue)
            .HasMaxLength(StringLengths.Length4000);
    }
}

public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.Property(rc => rc.ClaimType)
            .HasMaxLength(StringLengths.Length256);

        builder.Property(rc => rc.ClaimValue)
            .HasMaxLength(StringLengths.Length4000);
    }
}

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder.Property(ul => ul.LoginProvider)
            .HasMaxLength(StringLengths.Length128);

        builder.Property(ul => ul.ProviderKey)
            .HasMaxLength(StringLengths.Length128);

        builder.Property(ul => ul.ProviderDisplayName)
            .HasMaxLength(StringLengths.Length256);
    }
}

public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.Property(ut => ut.LoginProvider)
            .HasMaxLength(StringLengths.Length128);

        builder.Property(ut => ut.Name)
            .HasMaxLength(StringLengths.Length128);

        builder.Property(ut => ut.Value)
            .HasMaxLength(StringLengths.Length4000);
    }
}
