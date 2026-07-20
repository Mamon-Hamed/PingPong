using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Partners;

public sealed class PartnerServiceEntity : Entity<ServiceId>
{
    private PartnerServiceEntity(
        ServiceId id,
        string name,
        string media,
        decimal cost,
        double discountPercentage,
        PartnerId partnerId,
        CurrencyId currencyId)
        : base(id)
    {
        Name = name;
        Media = media;
        Cost = cost;
        DiscountPercentage = discountPercentage;
        PartnerId = partnerId;
        CurrencyId = currencyId;
    }

    private PartnerServiceEntity() { }

    public string Name { get; private set; } = string.Empty;
    public string Media { get; private set; } = string.Empty;
    public decimal Cost { get; private set; }
    public double DiscountPercentage { get; private set; }
    public decimal CostAfterDiscount => Cost * (decimal)(1 - DiscountPercentage / 100);

    public CurrencyId CurrencyId { get; private set; } = default!;
    public CurrencyEntity? Currency { get; private set; }

    public PartnerId PartnerId { get; private set; } = default!;
    public PartnerEntity? Partner { get; private set; }

    public static PartnerServiceEntity Create(
        string name,
        string media,
        decimal cost,
        double discountPercentage,
        PartnerId partnerId,
        CurrencyId currencyId)
    {
        return new PartnerServiceEntity(ServiceId.New(), name, media, cost, discountPercentage, partnerId, currencyId);
    }

    public void Update(
        string name,
        string media,
        decimal cost,
        double discountPercentage,
        CurrencyId currencyId)
    {
        Name = name;
        Media = media;
        Cost = cost;
        DiscountPercentage = discountPercentage;
        CurrencyId = currencyId;
    }
}
