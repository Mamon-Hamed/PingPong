using FluentValidation;
using PingPong.Domain.Constants;

namespace PingPong.Application.Features.Support.Update;

public sealed class UpdateSupportCommandValidator : AbstractValidator<UpdateSupportCommand>
{
    public UpdateSupportCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringLengths.Length200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(StringLengths.Length256);

        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(StringLengths.Length2000);
    }
}
