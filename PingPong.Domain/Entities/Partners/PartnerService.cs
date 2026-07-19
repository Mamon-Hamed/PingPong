using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Partners;

public sealed class PartnerService : Entity<ServiceId>
{
    private PartnerService(
        ServiceId id,
        string name,
        string media,
        decimal cost,
        double discountPercentage,
        PartnerId partnerId)
        : base(id)
    {
        Name = name;
        Media = media;
        Cost = cost;
        DiscountPercentage = discountPercentage;
        PartnerId = partnerId;
    }

    private PartnerService() { }

    public string Name { get; private set; } = string.Empty;
    public string Media { get; private set; } = string.Empty;
    public decimal Cost { get; private set; }
    public double DiscountPercentage { get; private set; }
    public decimal CostAfterDiscount => Cost * (decimal)(1 - DiscountPercentage / 100);

    public PartnerId PartnerId { get; private set; } = default!;
    public PartnerEntity? Partner { get; private set; }

    public static PartnerService Create(
        string name,
        string media,
        decimal cost,
        double discountPercentage,
        PartnerId partnerId)
    {
        return new PartnerService(ServiceId.New(), name, media, cost, discountPercentage, partnerId);
    }

    public void Update(
        string name,
        string media,
        decimal cost,
        double discountPercentage)
    {
        Name = name;
        Media = media;
        Cost = cost;
        DiscountPercentage = discountPercentage;
    }
}
