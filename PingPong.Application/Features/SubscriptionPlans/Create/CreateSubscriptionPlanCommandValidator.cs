using PingPong.Domain.Constants;

namespace PingPong.Application.Features.SubscriptionPlans.Create;

using FluentValidation;

public sealed class CreateSubscriptionPlanCommandValidator : AbstractValidator<CreateSubscriptionPlanCommand>
{
    public CreateSubscriptionPlanCommandValidator()
    {
        RuleFor(x => x.PlanName)
            .NotEmpty()
            .MaximumLength(StringLengths.Length200);
        RuleFor(x => x.BasePrice).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DiscountPercentage).InclusiveBetween(0, 100);
        RuleFor(x => x.DurationDays).GreaterThan(0);
    }
}
