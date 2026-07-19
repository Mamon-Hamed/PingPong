using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PingPong.Application.Abstractions.Authentication;

namespace PingPong.Infrastructure.Authentication;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private IReadOnlyCollection<Guid>? _favoritePartnerIds;

    public string? UserId =>
        httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? Email =>
        httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

    public double? Latitude =>
        double.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue("latitude") ?? string.Empty, out var lat) ? lat : null;

    public double? Longitude =>
        double.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue("longitude") ?? string.Empty, out var lon) ? lon : null;

    public bool IsAuthenticated =>
        httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public IReadOnlyCollection<Guid> FavoritePartnerIds
    {
        get
        {
            if (_favoritePartnerIds != null) return _favoritePartnerIds;

            var user = httpContextAccessor.HttpContext?.User;
            if (user == null || !IsAuthenticated)
            {
                _favoritePartnerIds = new List<Guid>().AsReadOnly();
                return _favoritePartnerIds;
            }

            var favoritePartnerIdClaims = user.FindAll("favorite_partner_id");
            _favoritePartnerIds = favoritePartnerIdClaims
                .Select(c => Guid.TryParse(c.Value, out var guid) ? guid : Guid.Empty)
                .Where(g => g != Guid.Empty)
                .ToList()
                .AsReadOnly();

            return _favoritePartnerIds;
        }
    }

    public bool IsInRole(string role) =>
        httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;
}
