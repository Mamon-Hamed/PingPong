namespace PingPong.Domain.StronglyTypes;

public sealed record CityId(Guid Value) : StronglyTypedId(Value)
{
    public static CityId New() => new(Guid.NewGuid());
}
