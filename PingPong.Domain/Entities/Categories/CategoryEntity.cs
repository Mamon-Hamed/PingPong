using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Categories;

using Primitives;

public sealed class CategoryEntity : AggregateRoot<CategoryId>
{
    private CategoryEntity(CategoryId id, string name, string? iconUrl, string? color)
        : base(id)
    {
        Name = name;
        IconUrl = iconUrl;
        Color = color;
    }

    private CategoryEntity()
    {
    }

    public string Name { get; private set; } = string.Empty;

    public string? IconUrl { get; private set; }

    public string? Color { get; private set; }

    public static CategoryEntity Create(string name, string? iconUrl, string? color)
    {
        var category = new CategoryEntity(CategoryId.New(), name, iconUrl, color);
        return category;
    }

    public void Update(string name, string? iconUrl, string? color)
    {
        Name = name;
        IconUrl = iconUrl;
        Color = color;
    }
}
