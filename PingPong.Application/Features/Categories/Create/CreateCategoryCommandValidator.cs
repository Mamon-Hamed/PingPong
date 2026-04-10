namespace PingPong.Application.Features.Categories.Create;

using FluentValidation;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.IconUrl).MaximumLength(2000);
    }
}
