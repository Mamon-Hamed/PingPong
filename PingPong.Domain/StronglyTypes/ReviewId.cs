namespace PingPong.Domain.StronglyTypes;

public sealed record ReviewId(Guid Value) : StronglyTypedId(Value)
{
    public static ReviewId New() => new(Guid.NewGuid());
}
