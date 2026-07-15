using FluentValidation;

namespace PingPong.Application.Features.Countries;

public sealed class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(10);
    }
}
