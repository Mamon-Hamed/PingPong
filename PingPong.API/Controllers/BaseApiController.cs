using MediatR;
using Microsoft.AspNetCore.Mvc;
using PingPong.Application;
using PingPong.Application.Common;

namespace PingPong.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return Ok(ApiResponse.Ok());

        return BadRequest(ApiResponse.Fail(400, result.Error!));
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(ApiResponse<T>.Ok(result.Value!));

        return BadRequest(ApiResponse<T>.Fail(400, result.Error!));
    }

    protected IActionResult HandleCreatedResult<T>(Result<T> result, string? routeName = null, object? routeValues = null)
    {
        if (result.IsSuccess)
        {
            var response = ApiResponse<T>.Created(result.Value!);

            if (routeName is not null)
                return CreatedAtRoute(routeName, routeValues, response);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        return BadRequest(ApiResponse<T>.Fail(400, result.Error!));
    }
}
