using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities;

public sealed class CityEntity : AggregateRoot<CityId>
{
    private CityEntity(CityId id, string name, CountryId countryId) : base(id)
    {
        Name = name;
        CountryId = countryId;
    }

    private CityEntity() { }

    public string Name { get; private set; } = string.Empty;
    public CountryId CountryId { get; private set; } = default!;
    public CountryEntity? Country { get; private set; }

    public static CityEntity Create(string name, CountryId countryId)
    {
        return new CityEntity(CityId.New(), name, countryId);
    }

    public void Update(string name, CountryId countryId)
    {
        Name = name;
        CountryId = countryId;
    }
}
