using Microsoft.AspNetCore.Authorization;
using PingPong.Application.Features.Support;
using PingPong.Application.Features.Support.Create;
using PingPong.Application.Features.Support.Delete;
using PingPong.Application.Features.Support.GetAll;
using PingPong.Application.Features.Support.GetById;
using PingPong.Application.Features.Support.Update;
using PingPong.Domain.StronglyTypes;

namespace PingPong.API.Controllers;

[Authorize]
public class SupportController : CrudController<SupportId, SupportResponse, CreateSupportCommand, UpdateSupportCommand, DeleteSupportCommand, GetSupportByIdQuery, GetAllSupportMessagesQuery>
{
}
