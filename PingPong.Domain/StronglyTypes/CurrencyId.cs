namespace PingPong.Domain.StronglyTypes;

public sealed record CurrencyId(Guid Value) : StronglyTypedId(Value)
{
    public static CurrencyId New() => new(Guid.CreateVersion7());
}