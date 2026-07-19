using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Features.Users;
using PingPong.Domain.Constants;

namespace PingPong.API.Controllers;

[Authorize(Roles = Roles.Admin)]
public class UsersController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(new GetUserByIdQuery(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await Mediator.SendCommandAsync(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendCommandAsync(new DeleteUserCommand(id), cancellationToken);
        return HandleResult(result);
    }
}
