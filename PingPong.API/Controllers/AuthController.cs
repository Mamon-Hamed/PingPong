using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Auth.Login;
using PingPong.Application.Auth.Register;
using PingPong.Application.Common;

namespace PingPong.API.Controllers;

public sealed class AuthController : BaseApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await Mediator.Send(command);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, ApiResponse.Created("Account created successfully."))
            : BadRequest(ApiResponse.Fail(400, result.Error!));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
