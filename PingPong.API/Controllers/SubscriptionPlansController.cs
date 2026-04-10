namespace PingPong.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.SubscriptionPlans.Create;
using Application.Features.SubscriptionPlans.Delete;
using Application.Features.SubscriptionPlans.GetAll;
using Application.Features.SubscriptionPlans.GetById;
using Application.Features.SubscriptionPlans.Update;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubscriptionPlansController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscriptionPlanCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendCommandAsync(command);
        if (result.IsFailure)
        {
            return HandleResult(result);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateSubscriptionPlanCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest("Id mismatch");
        }

        var result = await Mediator.SendCommandAsync(command);
        if (result.IsFailure)
        {
            return HandleResult(result);
        }

        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSubscriptionPlanCommand(id);
        var result = await Mediator.SendCommandAsync(command);
        if (result.IsFailure)
        {
            return HandleResult(result);
        }

        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSubscriptionPlanByIdQuery(id);
        var result = await Mediator.SendQueryAsync(query);
        if (result.IsFailure)
        {
            return HandleResult(result);
        }

        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllSubscriptionPlansQuery();
        var result = await Mediator.SendQueryAsync(query);
        if (result.IsFailure)
        {
            return HandleResult(result);
        }

        return HandleResult(result);
    }
}
