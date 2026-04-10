namespace PingPong.Application.Features.SubscriptionPlans.GetAll;

using Abstractions.Messaging;

public sealed record GetAllSubscriptionPlansQuery : GetAllQuery<SubscriptionPlanResponse>
{
    public string? PlanName { get;  set; } 

    public decimal? MinBasePrice { get;  set; }
    public decimal? MaxBasePrice { get;  set; }

    public decimal? MinDiscountPercentage { get;  set; }
    public decimal? MaxDiscountPercentage { get;  set; }

    public int? MinDurationDays { get;  set; }
    public int? MaxDurationDays { get;  set; }

    public decimal? MinTotalCost { get;  set; }
    public decimal? MaxTotalCost { get;  set; }
}
