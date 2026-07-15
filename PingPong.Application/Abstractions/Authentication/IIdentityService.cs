namespace PingPong.Application.Abstractions.Authentication;

public interface IIdentityService
{
    Task<IdentityRegistrationResponse> RegisterAsync(RegisterRequest request);

    Task<IdentityValidationResponse> ValidateCredentialsAsync(ValidateCredentialsRequest request);

    Task<bool> ValidateRefreshTokenAsync(ValidateRefreshTokenRequest request);

    Task UpdateRefreshTokenAsync(UpdateRefreshTokenRequest request);
}
