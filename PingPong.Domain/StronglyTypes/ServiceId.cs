namespace PingPong.Domain.StronglyTypes;

public sealed record ServiceId(Guid Value) : StronglyTypedId(Value)
{
    public static ServiceId New() => new(Guid.CreateVersion7());
}
