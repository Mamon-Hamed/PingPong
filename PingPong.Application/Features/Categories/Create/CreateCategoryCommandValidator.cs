using PingPong.Domain.Constants;

namespace PingPong.Application.Features.Categories.Create;

using FluentValidation;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringLengths.Length200);

        RuleFor(x => x.IconUrl)
            .MaximumLength(StringLengths.Length2000);
    }
}
