using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Authentication;

namespace PingPong.Infrastructure.Identity;

public sealed class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
{
    public async Task<(bool Succeeded, string UserId, string[] Errors)> RegisterAsync(
        string firstName, string lastName, string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await userManager.CreateAsync(user, password);

        return result.Succeeded
            ? (true, user.Id, Array.Empty<string>())
            : (false, string.Empty, result.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<(bool Succeeded, string UserId, string Email, IList<string> Roles)> ValidateCredentialsAsync(
        string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null || !user.IsActive)
            return (false, string.Empty, string.Empty, []);

        var isValidPassword = await userManager.CheckPasswordAsync(user, password);

        if (!isValidPassword)
            return (false, string.Empty, string.Empty, []);

        var roles = await userManager.GetRolesAsync(user);

        return (true, user.Id, user.Email!, roles);
    }

    public async Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken)
    {
        var user = await userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null || !user.IsActive)
            return false;

        var token = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

        if (token is null || !token.IsActive)
            return false;

        return true;
    }

    public async Task UpdateRefreshTokenAsync(string userId, string newRefreshToken, DateTime expiresAtUtc)
    {
        var user = await userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == userId);

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
            Token = newRefreshToken,
            ExpiresAtUtc = expiresAtUtc,
            UserId = userId
        });

        await userManager.UpdateAsync(user);
    }
}
