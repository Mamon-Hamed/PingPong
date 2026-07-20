namespace PingPong.Domain.StronglyTypes;

public sealed record CategoryId(Guid Value) : StronglyTypedId(Value)
{
    public static CategoryId New() => new(Guid.CreateVersion7());
}
