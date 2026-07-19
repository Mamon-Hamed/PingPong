using PingPong.Domain.Entities.Partners;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Identity;

public sealed class UserFavoritePartner
{
    public string UserId { get; private set; } = string.Empty;
    public ApplicationUser User { get; private set; } = null!;

    public PartnerId PartnerId { get; private set; } = default!;
    public PartnerEntity Partner { get; private set; } = default!;

    private UserFavoritePartner() { }

    public UserFavoritePartner(string userId, PartnerId partnerId)
    {
        UserId = userId;
        PartnerId = partnerId;
    }
}
