using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions.Authentication;

namespace PingPong.Infrastructure.Identity;

public sealed class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
{
    public async Task<IdentityRegistrationResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await userManager.CreateAsync(user, request.Password);

        return result.Succeeded
            ? new IdentityRegistrationResponse(true, user.Id, Array.Empty<string>())
            : new IdentityRegistrationResponse(false, string.Empty, result.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<IdentityValidationResponse> ValidateCredentialsAsync(ValidateCredentialsRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.IsActive)
            return new IdentityValidationResponse(false, string.Empty, string.Empty, string.Empty, []);

        var isValidPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isValidPassword)
            return new IdentityValidationResponse(false, string.Empty, string.Empty, string.Empty, []);

        var roles = await userManager.GetRolesAsync(user);

        return new IdentityValidationResponse(true, user.Id, user.Email!, user.FullName, roles);
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
