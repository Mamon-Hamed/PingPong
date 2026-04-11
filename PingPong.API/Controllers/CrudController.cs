using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.API.Controllers;

public abstract class CrudController<TId, TResponse, TCreateCommand, TUpdateCommand, TDeleteCommand, TGetByIdQuery, TGetAllQuery>(
    ) : BaseApiController
    where TId : StronglyTypedId
    where TCreateCommand : ICommand<Guid>
    where TUpdateCommand : ICommand
    where TDeleteCommand : DeleteCommand<TId>
    where TGetByIdQuery : GetByIdQuery<TId, TResponse>
    where TGetAllQuery : GetAllQuery<TResponse>
{
    [HttpPost]
    public virtual async Task<IActionResult> Create(TCreateCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendCommandAsync(command, cancellationToken)
            .ConfigureAwait(false);
        return await GetById(result.Value, cancellationToken);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(Guid id, TUpdateCommand command, CancellationToken cancellationToken)
    {
        // We can't easily check ID mismatch here because TUpdateCommand doesn't necessarily have an Id property in the interface
        // but we can assume it does or use reflection, or just let the handler handle it.
        // For now, let's keep it simple.
        var result = await Mediator.SendCommandAsync(command, cancellationToken)
            .ConfigureAwait(false);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = (TDeleteCommand)Activator.CreateInstance(typeof(TDeleteCommand), id)!;
        var result = await Mediator.SendCommandAsync(command, cancellationToken)
            .ConfigureAwait(false);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = (TGetByIdQuery)Activator.CreateInstance(typeof(TGetByIdQuery), id)!;
        var result = await Mediator.SendQueryAsync(query, cancellationToken)
            .ConfigureAwait(false);
        return HandleResult(result);
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll([FromQuery]TGetAllQuery query,CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(query, cancellationToken)
            .ConfigureAwait(false);
        return HandleResult(result);
    }
}
