using Cortex.Mediator.Processors;
using FluentValidation;
using ValidationException = PingPong.Domain.Exceptions.ValidationException;

namespace PingPong.Application.Behaviors;

public sealed class ValidationBehavior<TRequest>(IEnumerable<IValidator<TRequest>> validators)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public async Task ProcessAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return ;

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        if (failures.Count != 0)
            throw new ValidationException(failures);

    }
}
