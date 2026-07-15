using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Common;
using PingPong.Application.Features.Auth.Login;
using PingPong.Application.Features.Auth.RefreshToken;
using PingPong.Application.Features.Auth.Register;

namespace PingPong.API.Controllers;

public sealed class AuthController : BaseApiController
{
    [HttpPost("register/admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterCommand command)
    {
        var result = await Mediator.SendCommandAsync(command);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, ApiResponse.Created("Admin account created successfully."))
            : BadRequest(ApiResponse.Fail(400, result.Error!));
    }

    [HttpPost("register/user")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterCommand command)
    {
        var result = await Mediator.SendCommandAsync(command);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, ApiResponse.Created("User account created successfully."))
            : BadRequest(ApiResponse.Fail(400, result.Error!));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.SendCommandAsync(command);
        return HandleResult(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await Mediator.SendCommandAsync(command);
        return HandleResult(result);
    }
}
