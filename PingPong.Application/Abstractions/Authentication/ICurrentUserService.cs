namespace PingPong.Application.Abstractions.Authentication;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Email { get; }
    double? Latitude { get; }
    double? Longitude { get; }
    bool IsAuthenticated { get; }
    IReadOnlyCollection<Guid> FavoritePartnerIds { get; }
    bool IsInRole(string role);
}
