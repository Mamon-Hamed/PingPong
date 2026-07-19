using PingPong.Domain.Constants;

namespace PingPong.Application.Features.Categories.Update;

using FluentValidation;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringLengths.Length200);

        RuleFor(x => x.IconUrl)
            .MaximumLength(StringLengths.Length2000);
    }
}
