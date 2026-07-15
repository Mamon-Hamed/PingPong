using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Features.Countries;
using PingPong.Domain.Constants;
using PingPong.Domain.StronglyTypes;

namespace PingPong.API.Controllers;

[Authorize(Roles = Roles.Admin)]
public class CountriesController : CrudController<CountryId, CountryResponse, CreateCountryCommand, UpdateCountryCommand, DeleteCountryCommand, GetCountryByIdQuery, GetAllCountriesQuery>
{
    [HttpGet("lookup")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLookup(CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(new GetCountryLookupQuery(), cancellationToken);
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
    public override Task<IActionResult> GetAll([FromQuery] GetAllCountriesQuery query, CancellationToken cancellationToken)
    {
        return base.GetAll(query, cancellationToken);
    }
}
