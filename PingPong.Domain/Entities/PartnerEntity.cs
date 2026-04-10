using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities;

using Primitives;

public enum SubscriptionStatus
{
    Active,
    Trial,
    Expired,
    Canceled
}

public sealed class PartnerEntity : AggregateRoot<PartnerId>
{
    private PartnerEntity(
        PartnerId id,
        string companyName,
        string contactFirstName,
        string contactLastName,
        string phone,
        string email,
        string city,
        CategoryId categoryId,
        List<string> photos,
        bool isVerified,
        SubscriptionStatus subscriptionStatus)
        : base(id)
    {
        CompanyName = companyName;
        ContactFirstName = contactFirstName;
        ContactLastName = contactLastName;
        Phone = phone;
        Email = email;
        City = city;
        CategoryId = categoryId;
        Photos = photos;
        IsVerified = isVerified;
        SubscriptionStatus = subscriptionStatus;
    }

    private PartnerEntity()
    {
    }

    public string CompanyName { get; private set; } = string.Empty;

    public string ContactFirstName { get; private set; } = string.Empty;

    public string ContactLastName { get; private set; } = string.Empty;

    public string Phone { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string City { get; private set; } = string.Empty;

    public CategoryId CategoryId { get; private set; } = default!;

    public CategoryEntity? Category { get; private set; }

    public List<string> Photos { get; private set; } = [];

    public bool IsVerified { get; private set; }

    public SubscriptionStatus SubscriptionStatus { get; private set; }

    public static PartnerEntity Create(
        string companyName,
        string contactFirstName,
        string contactLastName,
        string phone,
        string email,
        string city,
        CategoryId categoryId,
        List<string> photos,
        bool isVerified,
        SubscriptionStatus subscriptionStatus)
    {
        var partner = new PartnerEntity(
            PartnerId.New(),
            companyName,
            contactFirstName,
            contactLastName,
            phone,
            email,
            city,
            categoryId,
            photos,
            isVerified,
            subscriptionStatus);

        return partner;
    }

    public void Update(
        string companyName,
        string contactFirstName,
        string contactLastName,
        string phone,
        string email,
        string city,
        CategoryId categoryId,
        List<string> photos,
        bool isVerified,
        SubscriptionStatus subscriptionStatus)
    {
        CompanyName = companyName;
        ContactFirstName = contactFirstName;
        ContactLastName = contactLastName;
        Phone = phone;
        Email = email;
        City = city;
        CategoryId = categoryId;
        Photos = photos;
        IsVerified = isVerified;
        SubscriptionStatus = subscriptionStatus;
    }
}
