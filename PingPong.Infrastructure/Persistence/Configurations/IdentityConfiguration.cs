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
        builder.Property(u => u.FirstName)
            .HasMaxLength(StringLengths.Length128);

        builder.Property(u => u.LastName)
            .HasMaxLength(StringLengths.Length128);

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
    }
}
