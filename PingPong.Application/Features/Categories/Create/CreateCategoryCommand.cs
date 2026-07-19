namespace PingPong.Application.Features.Categories.Create;

using Abstractions.Messaging;

public sealed record CreateCategoryCommand(
    string Name,
    string? IconUrl,
    string? Color) : ICommand<Guid>;
