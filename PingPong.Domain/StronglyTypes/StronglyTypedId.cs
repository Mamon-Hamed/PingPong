namespace PingPong.Domain.StronglyTypes;

public abstract record StronglyTypedId(Guid Value)
{
    public override string ToString() => Value.ToString();
}
