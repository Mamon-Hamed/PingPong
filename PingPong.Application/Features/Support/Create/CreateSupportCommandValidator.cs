using FluentValidation;
using PingPong.Domain.Constants;

namespace PingPong.Application.Features.Support.Create;

public sealed class CreateSupportCommandValidator : AbstractValidator<CreateSupportCommand>
{
    public CreateSupportCommandValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(StringLengths.Length2000);
    }
}
