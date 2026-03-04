namespace PingPong.Domain.Primitives;

public abstract record StronglyTypedId(Guid Value)
{
    public override string ToString() => Value.ToString();
}
