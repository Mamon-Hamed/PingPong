namespace PingPong.Domain.StronglyTypes;

public sealed record PartnerId(Guid Value) : StronglyTypedId(Value)
{
    public static PartnerId New() => new(Guid.NewGuid());
}
