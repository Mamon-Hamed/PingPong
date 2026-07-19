using PingPong.Domain.Constants;

namespace PingPong.Application.Features.SubscriptionPlans.Update;

using FluentValidation;

public sealed class UpdateSubscriptionPlanCommandValidator : AbstractValidator<UpdateSubscriptionPlanCommand>
{
    public UpdateSubscriptionPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PlanName)
            .NotEmpty()
            .MaximumLength(StringLengths.Length200);
        RuleFor(x => x.BasePrice).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DiscountPercentage).InclusiveBetween(0, 100);
        RuleFor(x => x.DurationDays).GreaterThan(0);
    }
}
