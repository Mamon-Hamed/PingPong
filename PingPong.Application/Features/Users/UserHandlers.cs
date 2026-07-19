using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Models;

namespace PingPong.Application.Features.Users;

public record GetUserByIdQuery(string Id) : IQuery<UserResponse>;

public record GetAllUsersQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<UserResponse>>;

public record UpdateUserCommand(
    string Id,
    string UserName,
    string Email,
    string? PhoneNumber,
    string? AvatarUrl,
    bool IsActive,
    Guid? CountryId,
    Guid? CityId) : ICommand;

public record DeleteUserCommand(string Id) : ICommand;

public sealed class UserHandlers(IIdentityService identityService)
    : IQueryHandler<GetUserByIdQuery, UserResponse>,
      IQueryHandler<GetAllUsersQuery, PaginatedList<UserResponse>>,
      ICommandHandler<UpdateUserCommand>,
      ICommandHandler<DeleteUserCommand>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await identityService.GetUserByIdAsync(request.Id);
        if (result.IsFailure)
        {
            return Result.Failure<UserResponse>(result.Error!);
        }

        return Result.Success(Map(result.Value!));
    }

    public async Task<Result<PaginatedList<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await identityService.GetAllUsersAsync(request.Page, request.PageSize);
        if (result.IsFailure)
        {
            return Result.Failure<PaginatedList<UserResponse>>(result.Error!);
        }

        var response = result.Value!.Items.Select(Map).ToList();
        return Result.Success(new PaginatedList<UserResponse>(response, result.Value.TotalItems, request.Page, request.PageSize));
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.UpdateUserAsync(new UpdateUserIdentityRequest(
            request.Id,
            request.UserName,
            request.Email,
            request.PhoneNumber,
            request.AvatarUrl,
            request.IsActive,
            request.CountryId,
            request.CityId));
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.DeleteUserAsync(request.Id);
    }

    private static UserResponse Map(UserIdentityResponse user) =>
        new(user.Id, user.UserName, user.Email, user.PhoneNumber, user.AvatarUrl, user.IsActive, user.CreatedAtUtc, user.LastLoginUtc, user.CountryId, user.CityId);
}
