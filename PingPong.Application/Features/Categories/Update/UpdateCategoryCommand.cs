namespace PingPong.Application.Features.Categories.Update;

using Abstractions.Messaging;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? IconUrl,
    string? Color) : ICommand;
