using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Players.Create;
using PingPong.Application.Players.GetById;

namespace PingPong.API.Controllers;

[Authorize]
public sealed class PlayersController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleCreatedResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetPlayerByIdQuery(id));
        return HandleResult(result);
    }
}
