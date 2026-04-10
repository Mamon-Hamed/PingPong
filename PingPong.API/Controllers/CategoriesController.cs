namespace PingPong.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Application.Features.Categories;
using Application.Features.Categories.Create;
using Application.Features.Categories.Delete;
using Application.Features.Categories.GetAll;
using Application.Features.Categories.GetById;
using Application.Features.Categories.Update;
using Domain.StronglyTypes;

[Authorize]
public class CategoriesController : CrudController<CategoryId, CategoryResponse, CreateCategoryCommand, UpdateCategoryCommand, DeleteCategoryCommand, GetCategoryByIdQuery, GetAllCategoriesQuery>
{
}
