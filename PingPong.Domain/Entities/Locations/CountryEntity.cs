using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Locations;

public sealed class CountryEntity : AggregateRoot<CountryId>
{
    private readonly List<CityEntity> _cities = [];

    private CountryEntity(CountryId id, string name, string code) : base(id)
    {
        Name = name;
        Code = code;
    }

    private CountryEntity() { }

    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;

    public IReadOnlyCollection<CityEntity> Cities => _cities.AsReadOnly();

    public static CountryEntity Create(string name, string code)
    {
        return new CountryEntity(CountryId.New(), name, code);
    }

    public void Update(string name, string code)
    {
        Name = name;
        Code = code;
    }
}
