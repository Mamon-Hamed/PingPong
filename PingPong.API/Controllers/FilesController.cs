using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Features.Files;
using PingPong.Application.Features.Files.RemoveImage;
using PingPong.Application.Features.Files.UploadImage;

namespace PingPong.API.Controllers;

public sealed class FilesController() : BaseApiController
{
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageCommand command)
    {
        var result = await Mediator.SendCommandAsync(command);
        return HandleResult(result);
    }

    [HttpDelete("remove-image")]
    public async Task<IActionResult> RemoveImage([FromBody] RemoveImageCommand command)
    {
        var result = await Mediator.SendCommandAsync(command);
        return HandleResult(result);
    }
}
