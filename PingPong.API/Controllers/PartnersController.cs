namespace PingPong.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Application.Features.Partners;
using Application.Features.Partners.Create;
using Application.Features.Partners.Delete;
using Application.Features.Partners.GetAll;
using Application.Features.Partners.GetById;
using Application.Features.Partners.Update;
using Domain.StronglyTypes;

using Microsoft.AspNetCore.Mvc;
using Application.Features.Partners.GetScroll;

[Authorize]
public class PartnersController : CrudController<PartnerId, PartnerDetailsResponse, CreatePartnerCommand, UpdatePartnerCommand, DeletePartnerCommand, GetPartnerByIdQuery, GetAllPartnersQuery>
{
    [HttpGet("scroll")]
    public async Task<IActionResult> GetScroll([FromQuery] GetPartnersScrollQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(query, cancellationToken);
        return HandleResult(result);
    }
}
