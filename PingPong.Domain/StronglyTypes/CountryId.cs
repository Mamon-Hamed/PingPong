namespace PingPong.Domain.StronglyTypes;

public sealed record CountryId(Guid Value) : StronglyTypedId(Value)
{
    public static CountryId New() => new(Guid.NewGuid());
}
