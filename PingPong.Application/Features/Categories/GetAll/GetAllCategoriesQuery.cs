using PingPong.Application.Abstractions.Messaging;

namespace PingPong.Application.Features.Categories.GetAll;

public sealed record GetAllCategoriesQuery : GetAllQuery<CategoryResponse>
{
    public string? Name { get; set; }
    
}
