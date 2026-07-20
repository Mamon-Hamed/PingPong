namespace PingPong.Domain.StronglyTypes;

public sealed record SupportId(Guid Value) : StronglyTypedId(Value)
{
    public static SupportId New() => new(Guid.CreateVersion7());
}
