namespace PingPong.Domain.StronglyTypes;

public sealed record NotificationId(Guid Value) : StronglyTypedId(Value)
{
    public static NotificationId New() => new(Guid.CreateVersion7());
}
