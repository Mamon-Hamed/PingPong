using FluentValidation;
using PingPong.Domain.Constants;

namespace PingPong.Application.Features.Countries;

public sealed class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringLengths.Length200);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(StringLengths.Length10);
    }
}
