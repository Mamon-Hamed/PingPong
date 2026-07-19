using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PingPong.Application.Features.Categories;
using PingPong.Application.Features.Categories.Create;
using PingPong.Application.Features.Categories.Delete;
using PingPong.Application.Features.Categories.GetAll;
using PingPong.Application.Features.Categories.GetById;
using PingPong.Application.Features.Categories.Update;
using PingPong.Domain.StronglyTypes;

namespace PingPong.API.Controllers;

[Authorize]
public class CategoriesController : CrudController<CategoryId, CategoryResponse, CreateCategoryCommand, UpdateCategoryCommand, DeleteCategoryCommand, GetCategoryByIdQuery, GetAllCategoriesQuery>
{
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookup(CancellationToken cancellationToken)
    {
        var result = await Mediator.SendQueryAsync(new GetCategoryLookupQuery(), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
