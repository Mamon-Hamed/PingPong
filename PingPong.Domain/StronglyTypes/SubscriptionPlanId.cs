namespace PingPong.Domain.StronglyTypes;

public sealed record SubscriptionPlanId(Guid Value) : StronglyTypedId(Value)
{
    public static SubscriptionPlanId New() => new(Guid.CreateVersion7());
}
