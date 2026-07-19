using FluentValidation;
using PingPong.Domain.Constants;

namespace PingPong.Application.Features.Partners.Create;

public sealed class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
{
    public CreatePartnerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringLengths.Length200);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(StringLengths.Length20);

        RuleFor(x => x.MediaUrl)
            .MaximumLength(StringLengths.Length2000);

        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.CountryId).NotEmpty();
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.SubscriptionStatus).IsInEnum();

        RuleFor(x => x.Location).NotNull();
        RuleFor(x => x.Location.City)
            .NotEmpty()
            .MaximumLength(StringLengths.Length100);
        RuleFor(x => x.Location.Country)
            .NotEmpty()
            .MaximumLength(StringLengths.Length100);
        RuleFor(x => x.Location.Address)
            .NotEmpty()
            .MaximumLength(StringLengths.Length500);
    }
}
