using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Features.Notifications.GetAll;
using PingPong.Application.Features.Notifications.CreateGeneral;

namespace PingPong.API.Controllers;

public sealed class NotificationsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(new GetAllNotificationsQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("general")]
    public async Task<IActionResult> CreateGeneral([FromBody] CreateGeneralNotificationCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendCommandAsync(command, cancellationToken);
        return HandleResult(result);
    }
}
