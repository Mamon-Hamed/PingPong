using PingPong.Application.Common;
using PingPong.Domain.Models;

namespace PingPong.Application.Abstractions.Authentication;

public interface IIdentityService
{
    Task<IdentityRegistrationResponse> RegisterAdminAsync(AdminRegisterRequest request);
    
    Task<IdentityRegistrationResponse> RegisterUserAsync(UserRegisterRequest request);

    Task<IdentityValidationResponse> ValidateCredentialsAsync(ValidateCredentialsRequest request);
    
    Task UpdateLastLoginAsync(string userId);

    Task<bool> ValidateRefreshTokenAsync(ValidateRefreshTokenRequest request);

    Task UpdateRefreshTokenAsync(UpdateRefreshTokenRequest request);

    Task<Result<UserIdentityResponse>> GetUserByIdAsync(string userId);
    
    Task<Result<PaginatedList<UserIdentityResponse>>> GetAllUsersAsync(int page, int pageSize);
    
    Task<Result> UpdateUserAsync(UpdateUserIdentityRequest request);
    
    Task<Result> DeleteUserAsync(string userId);
}
