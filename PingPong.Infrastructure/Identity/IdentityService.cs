using Microsoft.AspNetCore.Identity;
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
}
