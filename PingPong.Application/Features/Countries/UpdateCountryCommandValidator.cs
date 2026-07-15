using FluentValidation;

namespace PingPong.Application.Features.Countries;

public sealed class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(10);
    }
}
