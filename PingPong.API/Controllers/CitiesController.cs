using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Features.Cities;
using PingPong.Domain.Constants;
using PingPong.Domain.StronglyTypes;

namespace PingPong.API.Controllers;

[Authorize(Roles = Roles.Admin)]
public class CitiesController : CrudController<CityId, CityResponse, CreateCityCommand, UpdateCityCommand, DeleteCityCommand, GetCityByIdQuery, GetAllCitiesQuery>
{
    [HttpGet("lookup")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLookup([FromQuery] Guid? countryId, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(new GetCityLookupQuery(countryId), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public override Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return base.GetById(id, cancellationToken);
    }

    [HttpGet]
    [AllowAnonymous]
    public override Task<IActionResult> GetAll([FromQuery] GetAllCitiesQuery query, CancellationToken cancellationToken)
    {
        return base.GetAll(query, cancellationToken);
    }
}
