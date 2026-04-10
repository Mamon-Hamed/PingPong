namespace PingPong.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Application.Features.Partners;
using Application.Features.Partners.Create;
using Application.Features.Partners.Delete;
using Application.Features.Partners.GetAll;
using Application.Features.Partners.GetById;
using Application.Features.Partners.Update;
using Domain.StronglyTypes;

[Authorize]
public class PartnersController : CrudController<PartnerId, PartnerResponse, CreatePartnerCommand, UpdatePartnerCommand, DeletePartnerCommand, GetPartnerByIdQuery, GetAllPartnersQuery>
{
}
