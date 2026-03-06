namespace PingPong.Application.Abstractions.Authentication;

public interface IIdentityService
{
    Task<(bool Succeeded, string UserId, string[] Errors)> RegisterAsync(
        string firstName, string lastName, string email, string password);

    Task<(bool Succeeded, string UserId, string Email, IList<string> Roles)> ValidateCredentialsAsync(
        string email, string password);

    Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);

    Task UpdateRefreshTokenAsync(string userId, string newRefreshToken, DateTime expiresAtUtc);
}
