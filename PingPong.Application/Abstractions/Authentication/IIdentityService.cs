namespace PingPong.Application.Abstractions.Authentication;

public interface IIdentityService
{
    Task<IdentityRegistrationResponse> RegisterAdminAsync(AdminRegisterRequest request);
    
    Task<IdentityRegistrationResponse> RegisterUserAsync(UserRegisterRequest request);

    Task<IdentityValidationResponse> ValidateCredentialsAsync(ValidateCredentialsRequest request);
    
    Task UpdateLastLoginAsync(string userId);

    Task<bool> ValidateRefreshTokenAsync(ValidateRefreshTokenRequest request);

    Task UpdateRefreshTokenAsync(UpdateRefreshTokenRequest request);
}
