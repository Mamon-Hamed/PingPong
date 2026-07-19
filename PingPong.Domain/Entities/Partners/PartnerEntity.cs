using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Models;
using PingPong.Domain.Entities.Categories;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Events;

namespace PingPong.Domain.Entities.Partners;

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
    private readonly List<PartnerOpeningHour> _openingHours = [];
    private readonly List<PartnerService> _services = [];
    private readonly List<PartnerReview> _reviews = [];

    private PartnerEntity(
        PartnerId id,
        string name,
        string phone,
        string mediaUrl,
        DateTime? validUntil,
        List<string> gallery,
        CategoryId categoryId,
        CountryId countryId,
        CityId cityId,
        Location location,
        SubscriptionStatus subscriptionStatus)
        : base(id)
    {
        Name = name;
        Phone = phone;
        MediaUrl = mediaUrl;
        ValidUntil = validUntil;
        Gallery = gallery;
        CategoryId = categoryId;
        CountryId = countryId;
        CityId = cityId;
        Location = location;
        SubscriptionStatus = subscriptionStatus;
    }

    private PartnerEntity()
    {
    }

    public string Name { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string MediaUrl { get; private set; } = string.Empty;
    public DateTime? ValidUntil { get; private set; }
    public List<string> Gallery { get; private set; } = [];
    
    public CategoryId CategoryId { get; private set; } = default!;
    public CategoryEntity? Category { get; private set; }

    public CountryId CountryId { get; private set; } = default!;
    public CountryEntity? Country { get; private set; }

    public CityId CityId { get; private set; } = default!;
    public CityEntity? City { get; private set; }

    public Location Location { get; private set; } = default!;
    
    public int Views { get; private set; }
    public bool IsVerified { get; private set; }
    public SubscriptionStatus SubscriptionStatus { get; private set; }

    public IReadOnlyCollection<PartnerOpeningHour> OpeningHours => _openingHours.AsReadOnly();
    public IReadOnlyCollection<PartnerService> Services => _services.AsReadOnly();
    public IReadOnlyCollection<PartnerReview> Reviews => _reviews.AsReadOnly();

    public static PartnerEntity Create(
        string name,
        string phone,
        string mediaUrl,
        DateTime? validUntil,
        List<string> gallery,
        CategoryId categoryId,
        CountryId countryId,
        CityId cityId,
        Location location,
        SubscriptionStatus subscriptionStatus)
    {
        var partner = new PartnerEntity(
            PartnerId.New(),
            name,
            phone,
            mediaUrl,
            validUntil,
            gallery,
            categoryId,
            countryId,
            cityId,
            location,
            subscriptionStatus);
            
        partner.RaiseDomainEvent(new PartnerCreatedDomainEvent(partner.Id, partner.Name, partner.MediaUrl));
        
        return partner;
    }

    public void Update(
        string name,
        string phone,
        string mediaUrl,
        DateTime? validUntil,
        List<string> gallery,
        CategoryId categoryId,
        CountryId countryId,
        CityId cityId,
        Location location,
        SubscriptionStatus subscriptionStatus)
    {
        Name = name;
        Phone = phone;
        MediaUrl = mediaUrl;
        ValidUntil = validUntil;
        Gallery = gallery;
        CategoryId = categoryId;
        CountryId = countryId;
        CityId = cityId;
        Location = location;
        SubscriptionStatus = subscriptionStatus;
    }

    public void IncrementViews() => Views++;
    
    public void SetVerified(bool isVerified) => IsVerified = isVerified;

    public void AddService(string name, string media, decimal cost, double discountPercentage)
    {
        var service = PartnerService.Create(name, media, cost, discountPercentage, Id);
        _services.Add(service);
        RaiseDomainEvent(new PartnerServiceUpdatedDomainEvent(Id, Name, service.Id, service.Name));
    }

    public void AddOpeningHour(DayOfWeek day, string start, string end, bool isClosed)
    {
        var openingHour = PartnerOpeningHour.Create(day, start, end, isClosed, Id);
        _openingHours.Add(openingHour);
    }

    public void UpdateService(ServiceId serviceId, string name, string media, decimal cost, double discountPercentage)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        if (service != null)
        {
            service.Update(name, media, cost, discountPercentage);
            RaiseDomainEvent(new PartnerServiceUpdatedDomainEvent(Id, Name, service.Id, service.Name));
        }
    }
}
