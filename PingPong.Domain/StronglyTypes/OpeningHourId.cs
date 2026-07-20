namespace PingPong.Domain.StronglyTypes;

public sealed record OpeningHourId(Guid Value) : StronglyTypedId(Value)
{
    public static OpeningHourId New() => new(Guid.CreateVersion7());
}
