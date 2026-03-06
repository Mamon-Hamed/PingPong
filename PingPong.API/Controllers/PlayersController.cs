using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Features.Players.Create;
using PingPong.Application.Features.Players.GetById;

namespace PingPong.API.Controllers;

[Authorize]
public sealed class PlayersController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerCommand command)
    {
        var result = await Mediator.SendCommandAsync(command);
        return HandleCreatedResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.SendQueryAsync(new GetPlayerByIdQuery(id));
        return HandleResult(result);
    }
}
