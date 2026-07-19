using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Partners;

public sealed class PartnerReview : Entity<ReviewId>
{
    private PartnerReview(
        ReviewId id,
        string authorName,
        string authorAvatar,
        double rating,
        string comment,
        DateTime date,
        PartnerId partnerId,
        string userId)
        : base(id)
    {
        AuthorName = authorName;
        AuthorAvatar = authorAvatar;
        Rating = rating;
        Comment = comment;
        Date = date;
        PartnerId = partnerId;
        UserId = userId;
    }

    private PartnerReview() { }

    public string AuthorName { get; private set; } = string.Empty;
    public string AuthorAvatar { get; private set; } = string.Empty;
    public double Rating { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    
    public PartnerId PartnerId { get; private set; } = default!;
    public PartnerEntity? Partner { get; private set; }
    
    public string UserId { get; private set; } = string.Empty;

    public static PartnerReview Create(
        string authorName,
        string authorAvatar,
        double rating,
        string comment,
        DateTime date,
        PartnerId partnerId,
        string userId)
    {
        return new PartnerReview(ReviewId.New(), authorName, authorAvatar, rating, comment, date, partnerId, userId);
    }

    public void Update(string authorName, string authorAvatar, double rating, string comment)
    {
        AuthorName = authorName;
        AuthorAvatar = authorAvatar;
        Rating = rating;
        Comment = comment;
    }
}
